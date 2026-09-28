namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Soft rule del catálogo del Rule Engine v2: avisa sin bloquear; nunca es mandatory (SPEC-DOM-008 §2.1, ADR-009).
/// </summary>
public interface ISoftRule
{
    /// <summary>
    /// Código estable de la regla (<c>SR-xx</c>).
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Evalúa la regla sobre el candidato del contexto.
    /// </summary>
    /// <param name="context">Contexto compartido de evaluación.</param>
    /// <returns><c>null</c> si no hay nada que avisar; en caso contrario el aviso.</returns>
    RuleWarning? Evaluate(RuleEvaluationContext context);
}
