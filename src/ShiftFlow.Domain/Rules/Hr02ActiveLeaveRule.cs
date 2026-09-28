using ShiftFlow.Domain.Leaves;
using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.Domain.Rules;

/// <summary>
/// HR-02 — Un Leave activo bloquea la asignación (SPEC-DOM-006 §2.2, SPEC-DOM-008 §4.1).
/// </summary>
public sealed class Hr02ActiveLeaveRule : IHardRule
{
    /// <inheritdoc />
    public string Code => "HR-02";

    /// <inheritdoc />
    public bool IsMandatory => false;

    /// <inheritdoc />
    public RuleViolation? Evaluate(RuleEvaluationContext context)
    {
        if (context.ActiveLeaves is not { Count: > 0 } activeLeaves)
        {
            return null;
        }

        ShiftAssignment candidate = context.Candidate;

        // Leave Active cuya cobertura intersecta el intervalo candidato.
        foreach (Leave leave in activeLeaves)
        {
            if (leave.EmployeeId != candidate.EmployeeId)
            {
                continue;
            }

            if (leave.Status != LeaveStatus.Active)
            {
                continue;
            }

            if (leave.CoversInterval(candidate.StartAt, candidate.EndAt))
            {
                return new RuleViolation(
                    Code,
                    "Violación por ausencia: el empleado tiene un Leave activo que cubre el intervalo del turno.");
            }
        }

        return null;
    }
}
