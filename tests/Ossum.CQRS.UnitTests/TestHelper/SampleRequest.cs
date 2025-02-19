namespace Ossum.CQRS.UnitTests.TestHelper;

public record SampleRequest(string Name) : IRequest<int>;
