namespace Ossum.CQRS.UnitTests.Fakers;

public class TestRequestFaker : Faker<TestRequest>
{
    public TestRequestFaker()
    {
        RuleFor(e => e.Name, f => f.Internet.UserName());

        CustomInstantiator(f => new TestRequest(
            f.Internet.UserName()
        ));
    }
}
