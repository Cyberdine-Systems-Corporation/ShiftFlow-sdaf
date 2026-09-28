namespace ShiftFlow.Domain.Rules;

/// <summary>
/// SR-02 — Preferencia de tipología de turno (SPEC-DOM-008 §5). Stub del piloto: la semántica y el param
/// <c>PreferredShiftTypeIds</c> llegan con PBI-025; hasta entonces no emite avisos. Desactivada por defecto (§3).
/// </summary>
public sealed class Sr02ShiftTypePreferenceRule : ISoftRule
{
    /// <inheritdoc />
    public string Code => "SR-02";

    /// <inheritdoc />
    public RuleWarning? Evaluate(RuleEvaluationContext context) => null;
}
