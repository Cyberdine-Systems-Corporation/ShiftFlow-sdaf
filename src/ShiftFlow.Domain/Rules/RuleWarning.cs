namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Aviso de una soft rule del Rule Engine v2: informa, no bloquea la persistencia (SPEC-DOM-008 §1, ADR-009).
/// </summary>
/// <param name="Code">Código estable de regla (p. ej. <c>SR-01</c>).</param>
/// <param name="Message">Mensaje observable para API/UI.</param>
public sealed record RuleWarning(string Code, string Message);
