namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Rule Engine v2: orquesta el catálogo de hard rules (bloquean) y soft rules (avisan)
/// dentro del BC WorkforceScheduling (ADR-009 / SPEC-DOM-008; extiende ADR-003 / SPEC-DOM-006).
/// </summary>
public sealed class RuleEngine
{
    private readonly IReadOnlyList<IHardRule> _hardRules;
    private readonly IReadOnlyList<ISoftRule> _softRules;

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
    /// <exception cref="ArgumentException">Si dos reglas comparten código (códigos estables, SPEC-DOM-008 §6.5).</exception>
    public RuleEngine(IEnumerable<IHardRule> hardRules, IEnumerable<ISoftRule> softRules)
    {
        ArgumentNullException.ThrowIfNull(hardRules);
        ArgumentNullException.ThrowIfNull(softRules);

        _hardRules = hardRules.ToArray();
        _softRules = softRules.ToArray();

        HashSet<string> codes = new HashSet<string>(StringComparer.Ordinal);
        foreach (string code in _hardRules.Select(r => r.Code).Concat(_softRules.Select(r => r.Code)))
        {
            if (!codes.Add(code))
            {
                throw new ArgumentException($"Código de regla duplicado en el catálogo: {code}.");
            }
        }
    }

    /// <summary>
    /// Evalúa las hard rules activas (mandatory o enabled) y las soft rules enabled sobre el candidato del contexto.
    /// </summary>
    /// <param name="context">Contexto compartido con candidato, ventana cargada y overrides de <c>Enabled</c>.</param>
    /// <returns>
    /// Violaciones hard en orden de catálogo (lista vacía si ninguna) y avisos soft. Las soft se evalúan
    /// aunque haya violaciones hard; el caller decide si las expone (SPEC-APP-006 §5.5).
    /// </returns>
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
}
