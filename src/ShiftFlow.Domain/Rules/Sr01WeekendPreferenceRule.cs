namespace ShiftFlow.Domain.Rules;

/// <summary>
/// SR-01 — Preferencia fin de semana (SPEC-DOM-008 §5). Stub del piloto: la semántica y el param
/// <c>PreferFreeWeekend</c> llegan con PBI-024; hasta entonces no emite avisos. Desactivada por defecto (§3).
/// </summary>
public sealed class Sr01WeekendPreferenceRule : ISoftRule
{
    /// <inheritdoc />
    public string Code => "SR-01";

    /// <inheritdoc />
    public RuleWarning? Evaluate(RuleEvaluationContext context) => null;
}
