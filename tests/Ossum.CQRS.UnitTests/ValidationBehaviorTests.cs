namespace Ossum.CQRS.UnitTests;

public class ValidationBehaviorTests
{
    private readonly TestRequestFaker _testRequestFaker = new();
    private readonly Mock<RequestHandlerDelegate<int>> _nextMock = new();

    public ValidationBehaviorTests()
    {
        _nextMock.Setup(n => n()).ReturnsAsync(1);
    }

    [Fact]
    public async Task GivenValidRequestAndNoValidators_WhenHandlingRequest_ThenRequestIsProcessed()
    {
        // Given
        var request = _testRequestFaker.Generate();
        var validators = new List<IValidator<TestRequest>>();
        var behavior = new ValidationBehavior<TestRequest, int>(validators);

        // When
        var response = await behavior.Handle(request, _nextMock.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _nextMock.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task GivenValidRequestAndValidators_WhenHandlingRequest_ThenRequestIsProcessed()
    {
        // Given
        var request = _testRequestFaker.Generate();
        var validators = new List<IValidator<TestRequest>> { new TestRequestValidator() };
        var behavior = new ValidationBehavior<TestRequest, int>(validators);

        // When
        var response = await behavior.Handle(request, _nextMock.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _nextMock.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task GivenInvalidRequest_WhenHandlingRequest_ThenThrowsValidationException()
    {
        // Given
        var request = _testRequestFaker.RuleFor(e => e.Name, f => string.Empty).Generate();
        var validators = new List<IValidator<TestRequest>> { new TestRequestValidator() };
        var behavior = new ValidationBehavior<TestRequest, int>(validators);

        // When & Then
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => behavior.Handle(request, _nextMock.Object, CancellationToken.None));
        _nextMock.Verify(n => n(), Times.Never);
    }

    private class TestRequestValidator : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

}
