namespace Ossum.CQRS.UnitTests;

public class ValidationBehaviorTests
{
    private readonly Mock<RequestHandlerDelegate<int>> _delegate = new();

    public ValidationBehaviorTests()
    {
        _delegate.Setup(n => n()).ReturnsAsync(1);
    }

    [Fact]
    public async Task GivenValidRequestAndNoValidators_WhenHandling_ThenRequestIsProcessed()
    {
        // Given
        var request = new SampleRequestFaker().Generate();
        var validators = new List<IValidator<SampleRequest>>();
        var behavior = new ValidationBehavior<SampleRequest, int>(validators);

        // When
        var response = await behavior.Handle(request, _delegate.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _delegate.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task GivenValidRequestAndValidators_WhenHandling_ThenRequestIsProcessed()
    {
        // Given
        var request = new SampleRequestFaker().Generate();
        var validators = new List<IValidator<SampleRequest>> { new SampleRequestValidator() };
        var behavior = new ValidationBehavior<SampleRequest, int>(validators);

        // When
        var response = await behavior.Handle(request, _delegate.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _delegate.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task GivenInvalidRequest_WhenHandling_ThenThrowsValidationException()
    {
        // Given
        var request = new SampleRequestFaker().RuleFor(e => e.Name, f => string.Empty).Generate();
        var validators = new List<IValidator<SampleRequest>> { new SampleRequestValidator() };
        var behavior = new ValidationBehavior<SampleRequest, int>(validators);

        // When & Then
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => behavior.Handle(request, _delegate.Object, CancellationToken.None));
        _delegate.Verify(n => n(), Times.Never);
    }
}
