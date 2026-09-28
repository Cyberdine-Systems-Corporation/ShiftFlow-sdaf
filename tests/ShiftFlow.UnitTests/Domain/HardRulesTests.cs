using FluentAssertions;
using ShiftFlow.Domain.Leaves;
using ShiftFlow.Domain.Rules;
using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.UnitTests.Domain;

/// <summary>
/// Cada hard rule del catálogo evaluada por separado (PBI-016; SPEC-DOM-006 §2, SPEC-DOM-008 §4.1).
/// </summary>
public class HardRulesTests
{
    private static readonly Guid OrgId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid OtherEmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid ShiftTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    [Fact]
    public void HR01_es_mandatory_con_codigo_estable()
    {
        Hr01NoOverlapRule rule = new Hr01NoOverlapRule();

        rule.Code.Should().Be("HR-01");
        rule.IsMandatory.Should().BeTrue();
    }

    [Fact]
    public void HR01_devuelve_violacion_con_mensaje_v1_si_hay_solape()
    {
        RuleViolation? violation = new Hr01NoOverlapRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(12, 16), [CreateAssigned(10, 14)]));

        violation.Should().Be(new RuleViolation(
            "HR-01",
            "Violación de solape: la misma persona ya tiene un turno Assigned en un intervalo solapado."));
    }

    [Fact]
    public void HR01_permite_turnos_adyacentes()
    {
        RuleViolation? violation = new Hr01NoOverlapRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(14, 18), [CreateAssigned(10, 14)]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR01_ignora_turnos_de_otro_empleado()
    {
        ShiftAssignment otherEmployeeShift = CreateAssigned(10, 14, OtherEmployeeId);

        RuleViolation? violation = new Hr01NoOverlapRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(12, 16), [otherEmployeeShift]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR01_ignora_turnos_cancelados()
    {
        ShiftAssignment cancelled = CreateAssigned(10, 14);
        cancelled.Cancel();

        RuleViolation? violation = new Hr01NoOverlapRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(12, 16), [cancelled]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR02_no_es_mandatory_con_codigo_estable()
    {
        Hr02ActiveLeaveRule rule = new Hr02ActiveLeaveRule();

        rule.Code.Should().Be("HR-02");
        rule.IsMandatory.Should().BeFalse();
    }

    [Fact]
    public void HR02_devuelve_violacion_con_mensaje_v1_si_leave_activo_cubre_el_turno()
    {
        RuleViolation? violation = new Hr02ActiveLeaveRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(10, 14), [], [CreateLeave(EmployeeId)]));

        violation.Should().Be(new RuleViolation(
            "HR-02",
            "Violación por ausencia: el empleado tiene un Leave activo que cubre el intervalo del turno."));
    }

    [Fact]
    public void HR02_no_aplica_sin_leaves()
    {
        Hr02ActiveLeaveRule rule = new Hr02ActiveLeaveRule();

        rule.Evaluate(new RuleEvaluationContext(CreateAssigned(10, 14), [])).Should().BeNull();
        rule.Evaluate(new RuleEvaluationContext(CreateAssigned(10, 14), [], [])).Should().BeNull();
    }

    [Fact]
    public void HR02_ignora_leave_de_otro_empleado()
    {
        RuleViolation? violation = new Hr02ActiveLeaveRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(10, 14), [], [CreateLeave(OtherEmployeeId)]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR02_ignora_leave_cancelado()
    {
        Leave leave = CreateLeave(EmployeeId);
        leave.Cancel();

        RuleViolation? violation = new Hr02ActiveLeaveRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(10, 14), [], [leave]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR03_no_es_mandatory_con_codigo_estable()
    {
        Hr03MinimumRestRule rule = new Hr03MinimumRestRule();

        rule.Code.Should().Be("HR-03");
        rule.IsMandatory.Should().BeFalse();
    }

    [Fact]
    public void HR03_devuelve_violacion_con_mensaje_v1_si_gap_inferior_al_umbral()
    {
        RuleViolation? violation = new Hr03MinimumRestRule().Evaluate(
            new RuleEvaluationContext(
                CreateAssigned(16, 20),
                [CreateAssigned(8, 16)],
                MinimumRest: TimeSpan.FromMinutes(660)));

        violation.Should().Be(new RuleViolation(
            "HR-03",
            "Violación de descanso mínimo: el intervalo respecto a otro turno Assigned es inferior al umbral de la organización."));
    }

    [Fact]
    public void HR03_evalua_tambien_el_turno_posterior_al_candidato()
    {
        RuleViolation? violation = new Hr03MinimumRestRule().Evaluate(
            new RuleEvaluationContext(
                CreateAssigned(8, 12),
                [CreateAssigned(14, 18)],
                MinimumRest: TimeSpan.FromMinutes(660)));

        violation.Should().NotBeNull().And.Match<RuleViolation>(v => v.Code == "HR-03");
    }

    [Fact]
    public void HR03_no_aplica_sin_umbral()
    {
        RuleViolation? violation = new Hr03MinimumRestRule().Evaluate(
            new RuleEvaluationContext(CreateAssigned(16, 20), [CreateAssigned(8, 16)]));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR03_deja_el_solape_a_HR01()
    {
        RuleViolation? violation = new Hr03MinimumRestRule().Evaluate(
            new RuleEvaluationContext(
                CreateAssigned(12, 16),
                [CreateAssigned(10, 14)],
                MinimumRest: TimeSpan.FromMinutes(660)));

        violation.Should().BeNull();
    }

    [Fact]
    public void HR03_ignora_turnos_de_otro_empleado_y_cancelados()
    {
        ShiftAssignment cancelled = CreateAssigned(8, 16);
        cancelled.Cancel();

        RuleViolation? violation = new Hr03MinimumRestRule().Evaluate(
            new RuleEvaluationContext(
                CreateAssigned(16, 20),
                [CreateAssigned(8, 16, OtherEmployeeId), cancelled],
                MinimumRest: TimeSpan.FromMinutes(660)));

        violation.Should().BeNull();
    }

    private static ShiftAssignment CreateAssigned(int startHour, int endHour, Guid? employeeId = null) =>
        ShiftAssignment.Create(
            OrgId,
            employeeId ?? EmployeeId,
            OrgId,
            employeeIsActive: true,
            ShiftTypeId,
            OrgId,
            shiftTypeIsActive: true,
            new DateTimeOffset(2026, 8, 10, startHour, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2026, 8, 10, endHour, 0, 0, TimeSpan.Zero));

    private static Leave CreateLeave(Guid employeeId) =>
        Leave.Create(
            OrgId,
            employeeId,
            OrgId,
            employeeIsActive: true,
            new DateOnly(2026, 8, 10),
            new DateOnly(2026, 8, 10));
}
