namespace Ossum.CQRS.UnitTests.TestHelper;

public class SampleRequestValidator : AbstractValidator<SampleRequest>
{
    public SampleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
