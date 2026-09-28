using ShiftFlow.Domain.Leaves;
using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Contexto compartido por las reglas del catálogo: el caller carga la ventana una sola vez (SPEC-DOM-008 §2.1, §6.4).
/// </summary>
/// <param name="Candidate">Asignación candidata (aún no persistida o no confirmada).</param>
/// <param name="ExistingAssigned">Asignaciones <see cref="ShiftAssignmentStatus.Assigned"/> del mismo empleado.</param>
/// <param name="ActiveLeaves">Leaves <see cref="LeaveStatus.Active"/> del mismo empleado (<c>null</c> o vacío si no hay).</param>
/// <param name="MinimumRest">Umbral de descanso mínimo (HR-03); <c>null</c> o cero no aplica la regla.</param>
/// <param name="EnabledOverrides">
/// <c>Enabled</c> por código de regla. Sin clave (o <c>null</c>) rige el default del catálogo: hard activas,
/// soft desactivadas; una hard mandatory se evalúa siempre (SPEC-DOM-008 §3).
/// </param>
public sealed record RuleEvaluationContext(
    ShiftAssignment Candidate,
    IReadOnlyList<ShiftAssignment> ExistingAssigned,
    IReadOnlyList<Leave>? ActiveLeaves = null,
    TimeSpan? MinimumRest = null,
    IReadOnlyDictionary<string, bool>? EnabledOverrides = null);
