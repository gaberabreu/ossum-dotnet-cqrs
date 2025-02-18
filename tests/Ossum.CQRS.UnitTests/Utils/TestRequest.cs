namespace Ossum.CQRS.UnitTests.Utils;

public record TestRequest(string Name) : IRequest<int>;
