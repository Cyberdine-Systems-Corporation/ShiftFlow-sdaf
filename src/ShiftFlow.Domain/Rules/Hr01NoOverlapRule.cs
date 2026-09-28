using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.Domain.Rules;

/// <summary>
/// HR-01 — No solape de turnos para la misma persona. Mandatory: siempre se evalúa (SPEC-DOM-006 §2.1, SPEC-DOM-008 §4.1).
/// </summary>
public sealed class Hr01NoOverlapRule : IHardRule
{
    /// <inheritdoc />
    public string Code => "HR-01";

    /// <inheritdoc />
    public bool IsMandatory => true;

    /// <inheritdoc />
    public RuleViolation? Evaluate(RuleEvaluationContext context)
    {
        ShiftAssignment candidate = context.Candidate;

        // Intervalos semiabiertos [StartAt, EndAt); el borde exacto no solapa.
        foreach (ShiftAssignment existing in context.ExistingAssigned)
        {
            if (existing.EmployeeId != candidate.EmployeeId)
            {
                continue;
            }

            if (existing.Status != ShiftAssignmentStatus.Assigned)
            {
                continue;
            }

            if (ShiftIntervals.Overlaps(candidate.StartAt, candidate.EndAt, existing.StartAt, existing.EndAt))
            {
                return new RuleViolation(
                    Code,
                    "Violación de solape: la misma persona ya tiene un turno Assigned en un intervalo solapado.");
            }
        }

        return null;
    }
}
