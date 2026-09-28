namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Registro del catálogo del Rule Engine v2 (SPEC-DOM-008 §4.1 y §5). El orden es el de evaluación:
/// HR-01, HR-02, HR-03 como en v1, de modo que la primera violación sigue siendo la misma (SPEC-DOM-006).
/// </summary>
public static class RuleCatalog
{
    /// <summary>
    /// Hard rules MVP migradas (HR-01 mandatory; HR-02 y HR-03 activas por defecto).
    /// </summary>
    public static IReadOnlyList<IHardRule> HardRules { get; } =
    [
        new Hr01NoOverlapRule(),
        new Hr02ActiveLeaveRule(),
        new Hr03MinimumRestRule(),
    ];

    /// <summary>
    /// Soft rules del piloto (stubs hasta PBI-024 y PBI-025); desactivadas por defecto.
    /// </summary>
    public static IReadOnlyList<ISoftRule> SoftRules { get; } =
    [
        new Sr01WeekendPreferenceRule(),
        new Sr02ShiftTypePreferenceRule(),
    ];
}
