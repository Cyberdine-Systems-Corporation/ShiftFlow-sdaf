namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Resultado del Rule Engine v2 (SPEC-DOM-008 §1, ADR-009).
/// </summary>
/// <param name="HardViolations">Violaciones hard en orden de catálogo; cualquiera impide persistir.</param>
/// <param name="SoftWarnings">Avisos soft; informativos, no impiden persistir.</param>
public sealed record RuleEvaluationResult(
    IReadOnlyList<RuleViolation> HardViolations,
    IReadOnlyList<RuleWarning> SoftWarnings);
