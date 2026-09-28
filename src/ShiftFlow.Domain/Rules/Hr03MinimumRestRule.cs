using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.Domain.Rules;

/// <summary>
/// HR-03 — Descanso mínimo entre turnos; umbral <see cref="RuleEvaluationContext.MinimumRest"/>
/// de la Organization (SPEC-DOM-006 §2.3, SPEC-DOM-008 §4.1).
/// </summary>
public sealed class Hr03MinimumRestRule : IHardRule
{
    /// <inheritdoc />
    public string Code => "HR-03";

    /// <inheritdoc />
    public bool IsMandatory => false;

    /// <inheritdoc />
    public RuleViolation? Evaluate(RuleEvaluationContext context)
    {
        // Umbral null o cero: la regla no aplica.
        if (context.MinimumRest is not { } rest || rest <= TimeSpan.Zero)
        {
            return null;
        }

        ShiftAssignment candidate = context.Candidate;

        // Gap entre turnos Assigned no solapados < umbral (el solape es territorio de HR-01).
        foreach (ShiftAssignment existing in context.ExistingAssigned)
        {
            if (existing.EmployeeId != candidate.EmployeeId
                || existing.Status != ShiftAssignmentStatus.Assigned)
            {
                continue;
            }

            if (ShiftIntervals.Overlaps(candidate.StartAt, candidate.EndAt, existing.StartAt, existing.EndAt))
            {
                continue;
            }

            TimeSpan gap = ShiftIntervals.GapBetween(candidate.StartAt, candidate.EndAt, existing.StartAt, existing.EndAt);
            if (gap < rest)
            {
                return new RuleViolation(
                    Code,
                    "Violación de descanso mínimo: el intervalo respecto a otro turno Assigned es inferior al umbral de la organización.");
            }
        }

        return null;
    }
}
