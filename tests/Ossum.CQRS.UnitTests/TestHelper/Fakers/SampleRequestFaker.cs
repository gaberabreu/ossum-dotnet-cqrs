namespace Ossum.CQRS.UnitTests.TestHelper.Fakers;

public class SampleRequestFaker : Faker<SampleRequest>
{
    public SampleRequestFaker()
    {
        RuleFor(e => e.Name, f => f.Internet.UserName());

        CustomInstantiator(f => new SampleRequest(
            f.Internet.UserName()
        ));
    }
}
