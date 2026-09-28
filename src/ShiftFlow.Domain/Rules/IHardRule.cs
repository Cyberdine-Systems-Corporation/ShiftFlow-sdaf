namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Hard rule del catálogo del Rule Engine v2: una violación bloquea la persistencia (SPEC-DOM-008 §2.1, ADR-009).
/// </summary>
public interface IHardRule
{
    /// <summary>
    /// Código estable de la regla (<c>HR-xx</c>).
    /// </summary>
    string Code { get; }

    /// <summary>
    /// <c>true</c> si la regla se evalúa siempre y la configuración no puede desactivarla (p. ej. <c>HR-01</c>).
    /// </summary>
    bool IsMandatory { get; }

    /// <summary>
    /// Evalúa la regla sobre el candidato del contexto.
    /// </summary>
    /// <param name="context">Contexto compartido de evaluación.</param>
    /// <returns><c>null</c> si la regla se cumple; en caso contrario la violación.</returns>
    RuleViolation? Evaluate(RuleEvaluationContext context);
}
