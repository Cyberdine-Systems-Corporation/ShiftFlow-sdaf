using FluentAssertions;
using ShiftFlow.Domain.Leaves;
using ShiftFlow.Domain.Rules;
using ShiftFlow.Domain.ShiftAssignments;

namespace ShiftFlow.UnitTests.Domain;

/// <summary>
/// Catálogo y orquestación del Rule Engine v2 (PBI-016; SPEC-DOM-008 §1, §2.1, §3, §6).
/// </summary>
public class RuleEngineCatalogTests
{
    private static readonly Guid OrgId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid ShiftTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly TimeSpan ElevenHours = TimeSpan.FromMinutes(660);

    [Fact]
    public void Catalogo_hard_contiene_HR01_HR02_HR03_en_orden_v1_y_solo_HR01_mandatory()
    {
        RuleCatalog.HardRules.Select(r => r.Code).Should().Equal("HR-01", "HR-02", "HR-03");
        RuleCatalog.HardRules.Where(r => r.IsMandatory).Select(r => r.Code).Should().Equal("HR-01");
    }

    [Fact]
    public void Catalogo_soft_contiene_stubs_SR01_y_SR02_que_no_avisan()
    {
        RuleEvaluationContext context = new RuleEvaluationContext(CreateAssigned(10, 14), []);

        RuleCatalog.SoftRules.Select(r => r.Code).Should().Equal("SR-01", "SR-02");
        RuleCatalog.SoftRules.Should().OnlyContain(r => r.Evaluate(context) == null);
    }

    [Fact]
    public void Sin_violaciones_el_resultado_esta_vacio()
    {
        RuleEvaluationResult result = new RuleEngine().Evaluate(
            new RuleEvaluationContext(CreateAssigned(10, 14), [], [], ElevenHours));

        result.HardViolations.Should().BeEmpty();
        result.SoftWarnings.Should().BeEmpty();
    }

    [Fact]
    public void Devuelve_varias_violaciones_hard_juntas_en_orden_de_catalogo()
    {
        // Candidato 10-14: solapa con 8-12 (HR-01), cae bajo leave (HR-02) y deja 2 h hasta 16-18 (HR-03).
        RuleEvaluationContext context = new RuleEvaluationContext(
            CreateAssigned(10, 14),
            [CreateAssigned(8, 12), CreateAssigned(16, 18)],
            [CreateLeave()],
            ElevenHours);

        RuleEvaluationResult result = new RuleEngine().Evaluate(context);

        result.HardViolations.Select(v => v.Code).Should().Equal("HR-01", "HR-02", "HR-03");
        result.SoftWarnings.Should().BeEmpty();
    }

    [Fact]
    public void Soft_desactivadas_por_defecto_no_aportan_avisos()
    {
        RuleEngine engine = new RuleEngine(RuleCatalog.HardRules, [new AlwaysWarnSoftRule("SR-99")]);

        RuleEvaluationResult result = engine.Evaluate(new RuleEvaluationContext(CreateAssigned(10, 14), []));

        result.SoftWarnings.Should().BeEmpty();
        result.HardViolations.Should().BeEmpty();
    }

    [Fact]
    public void Soft_enabled_avisa_sin_impedir_el_ok_de_hard()
    {
        RuleEngine engine = new RuleEngine(RuleCatalog.HardRules, [new AlwaysWarnSoftRule("SR-99")]);

        RuleEvaluationResult result = engine.Evaluate(new RuleEvaluationContext(
            CreateAssigned(10, 14),
            [],
            EnabledOverrides: new Dictionary<string, bool> { ["SR-99"] = true }));

        result.HardViolations.Should().BeEmpty();
        result.SoftWarnings.Should().ContainSingle().Which.Code.Should().Be("SR-99");
    }

    [Fact]
    public void Soft_enabled_no_oculta_la_violacion_hard()
    {
        RuleEngine engine = new RuleEngine(RuleCatalog.HardRules, [new AlwaysWarnSoftRule("SR-99")]);

        RuleEvaluationResult result = engine.Evaluate(new RuleEvaluationContext(
            CreateAssigned(12, 16),
            [CreateAssigned(10, 14)],
            EnabledOverrides: new Dictionary<string, bool> { ["SR-99"] = true }));

        result.HardViolations.Should().ContainSingle(v => v.Code == "HR-01");
        result.SoftWarnings.Should().ContainSingle(w => w.Code == "SR-99");
    }

    [Fact]
    public void HR01_no_se_omite_aunque_se_desactive()
    {
        RuleEvaluationResult result = new RuleEngine().Evaluate(new RuleEvaluationContext(
            CreateAssigned(12, 16),
            [CreateAssigned(10, 14)],
            EnabledOverrides: new Dictionary<string, bool> { ["HR-01"] = false }));

        result.HardViolations.Should().ContainSingle(v => v.Code == "HR-01");
    }

    [Fact]
    public void HR02_desactivada_no_se_evalua()
    {
        RuleEvaluationResult result = new RuleEngine().Evaluate(new RuleEvaluationContext(
            CreateAssigned(10, 14),
            [],
            [CreateLeave()],
            EnabledOverrides: new Dictionary<string, bool> { ["HR-02"] = false }));

        result.HardViolations.Should().BeEmpty();
    }

    [Fact]
    public void HR03_desactivada_no_se_evalua()
    {
        RuleEvaluationResult result = new RuleEngine().Evaluate(new RuleEvaluationContext(
            CreateAssigned(16, 20),
            [CreateAssigned(8, 16)],
            MinimumRest: ElevenHours,
            EnabledOverrides: new Dictionary<string, bool> { ["HR-03"] = false }));

        result.HardViolations.Should().BeEmpty();
    }

    [Fact]
    public void Override_enabled_de_hard_mantiene_el_comportamiento_v1()
    {
        RuleEvaluationResult result = new RuleEngine().Evaluate(new RuleEvaluationContext(
            CreateAssigned(16, 20),
            [CreateAssigned(8, 16)],
            MinimumRest: ElevenHours,
            EnabledOverrides: new Dictionary<string, bool> { ["HR-03"] = true }));

        result.HardViolations.Should().ContainSingle(v => v.Code == "HR-03");
    }

    [Fact]
    public void Rechaza_catalogo_con_codigos_duplicados()
    {
        Action act = () => _ = new RuleEngine(RuleCatalog.HardRules, [new AlwaysWarnSoftRule("HR-01")]);

        act.Should().Throw<ArgumentException>().WithMessage("*HR-01*");
    }

    private static ShiftAssignment CreateAssigned(int startHour, int endHour) =>
        ShiftAssignment.Create(
            OrgId,
            EmployeeId,
            OrgId,
            employeeIsActive: true,
            ShiftTypeId,
            OrgId,
            shiftTypeIsActive: true,
            new DateTimeOffset(2026, 8, 10, startHour, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2026, 8, 10, endHour, 0, 0, TimeSpan.Zero));

    private static Leave CreateLeave() =>
        Leave.Create(
            OrgId,
            EmployeeId,
            OrgId,
            employeeIsActive: true,
            new DateOnly(2026, 8, 10),
            new DateOnly(2026, 8, 10));

    /// <summary>
    /// Soft rule de prueba que siempre avisa: el catálogo piloto solo tiene stubs (SR-01/SR-02 llegan en PBI-024/025).
    /// </summary>
    private sealed class AlwaysWarnSoftRule(string code) : ISoftRule
    {
        public string Code { get; } = code;

        public RuleWarning? Evaluate(RuleEvaluationContext context) =>
            new RuleWarning(Code, "Aviso de prueba.");
    }
}
