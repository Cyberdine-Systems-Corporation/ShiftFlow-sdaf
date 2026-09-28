namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Rule Engine v2: orquesta el catálogo de hard rules (bloquean) y soft rules (avisan)
/// dentro del BC WorkforceScheduling (ADR-009 / SPEC-DOM-008; extiende ADR-003 / SPEC-DOM-006).
/// </summary>
public sealed class RuleEngine
{
    private readonly IReadOnlyList<IHardRule> _hardRules;
    private readonly IReadOnlyList<ISoftRule> _softRules;

    #region Factory

    /// <summary>
    /// Crea el motor con el catálogo por defecto (<see cref="RuleCatalog"/>).
    /// </summary>
    public RuleEngine()
        : this(RuleCatalog.HardRules, RuleCatalog.SoftRules)
    {
    }

    /// <summary>
    /// Crea el motor con un catálogo explícito; el orden de cada lista es el orden de evaluación.
    /// </summary>
    /// <param name="hardRules">Hard rules del catálogo.</param>
    /// <param name="softRules">Soft rules del catálogo.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="hardRules"/> o <paramref name="softRules"/> es <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// Si el catálogo contiene una regla <c>null</c>, una regla sin código (nulo o en blanco) o dos reglas que comparten
    /// código (códigos estables, SPEC-DOM-008 §6.5).
    /// </exception>
    public RuleEngine(IEnumerable<IHardRule> hardRules, IEnumerable<ISoftRule> softRules)
    {
        ArgumentNullException.ThrowIfNull(hardRules);
        ArgumentNullException.ThrowIfNull(softRules);

        _hardRules = hardRules.ToArray();
        _softRules = softRules.ToArray();

        EnsureNoNullRules(_hardRules, nameof(hardRules));
        EnsureNoNullRules(_softRules, nameof(softRules));
        EnsureValidCodes(_hardRules.Select(r => r.Code).Concat(_softRules.Select(r => r.Code)));
    }

    #endregion

    #region Behavior

    /// <summary>
    /// Evalúa las hard rules activas (mandatory o enabled) y las soft rules enabled sobre el candidato del contexto.
    /// </summary>
    /// <param name="context">Contexto compartido con candidato, ventana cargada y overrides de <c>Enabled</c>.</param>
    /// <returns>
    /// Violaciones hard en orden de catálogo (lista vacía si ninguna) y avisos soft. Las soft se evalúan
    /// aunque haya violaciones hard; el caller decide si las expone (SPEC-APP-006 §5.5).
    /// </returns>
    /// <exception cref="ArgumentNullException">Si <paramref name="context"/> es <c>null</c>.</exception>
    public RuleEvaluationResult Evaluate(RuleEvaluationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        List<RuleViolation> violations = new List<RuleViolation>();
        foreach (IHardRule rule in _hardRules)
        {
            if (!rule.IsMandatory && !IsEnabled(rule.Code, context, defaultEnabled: true))
            {
                continue;
            }

            if (rule.Evaluate(context) is { } violation)
            {
                violations.Add(violation);
            }
        }

        List<RuleWarning> warnings = new List<RuleWarning>();
        foreach (ISoftRule rule in _softRules)
        {
            if (!IsEnabled(rule.Code, context, defaultEnabled: false))
            {
                continue;
            }

            if (rule.Evaluate(context) is { } warning)
            {
                warnings.Add(warning);
            }
        }

        return new RuleEvaluationResult(violations, warnings);
    }

    /// <summary>
    /// Resuelve <c>Enabled</c>: el override del contexto si existe; si no, el default del catálogo (SPEC-DOM-008 §3).
    /// </summary>
    private static bool IsEnabled(string code, RuleEvaluationContext context, bool defaultEnabled) =>
        context.EnabledOverrides is { } overrides && overrides.TryGetValue(code, out bool enabled)
            ? enabled
            : defaultEnabled;

    #endregion

    #region Invariants

    private static void EnsureNoNullRules<TRule>(IReadOnlyList<TRule> rules, string paramName)
        where TRule : class
    {
        // El tipo no admite null, pero un caller sin nullable habilitado puede pasarlo: fallar aquí con
        // ArgumentException en vez de NullReferenceException al leer el código (hallazgo R3-null-rule-element).
        if (rules.Any(r => r is null))
        {
            throw new ArgumentException("El catálogo contiene una regla nula.", paramName);
        }
    }

    private static void EnsureValidCodes(IEnumerable<string> codes)
    {
        HashSet<string> seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (string code in codes)
        {
            // Un código nulo pasaría el HashSet y fallaría después en IsEnabled (hallazgo R3-null-rule-code).
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El catálogo contiene una regla sin código.");
            }

            if (!seen.Add(code))
            {
                throw new ArgumentException($"Código de regla duplicado en el catálogo: {code}.");
            }
        }
    }

    #endregion
}
