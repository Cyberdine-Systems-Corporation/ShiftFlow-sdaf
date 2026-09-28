namespace ShiftFlow.Domain.Rules;

/// <summary>
/// Aritmética de intervalos semiabiertos <c>[StartAt, EndAt)</c> compartida por las hard rules.
/// </summary>
internal static class ShiftIntervals
{
    /// <summary>
    /// Determina si dos intervalos semiabiertos se solapan.
    /// </summary>
    internal static bool Overlaps(
        DateTimeOffset startA,
        DateTimeOffset endA,
        DateTimeOffset startB,
        DateTimeOffset endB) =>
        startA < endB && startB < endA;

    /// <summary>
    /// Tiempo entre el fin de un intervalo y el inicio del otro (sin solape).
    /// </summary>
    internal static TimeSpan GapBetween(
        DateTimeOffset startA,
        DateTimeOffset endA,
        DateTimeOffset startB,
        DateTimeOffset endB)
    {
        if (endA <= startB)
        {
            return startB - endA;
        }

        if (endB <= startA)
        {
            return startA - endB;
        }

        return TimeSpan.Zero;
    }
}
