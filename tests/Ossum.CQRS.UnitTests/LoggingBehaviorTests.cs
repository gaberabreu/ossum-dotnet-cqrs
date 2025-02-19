namespace Ossum.CQRS.UnitTests;

public class LoggingBehaviorTests
{
    private readonly Mock<ILogger<Mediator>> _logger = new();
    private readonly Mock<RequestHandlerDelegate<int>> _delegate = new();
    private readonly LoggingBehavior<SampleRequest, int> _behavior;

    public LoggingBehaviorTests()
    {
        _behavior = new LoggingBehavior<SampleRequest, int>(_logger.Object);
    }

    [Fact]
    public async Task GivenLoggingBehavior_WhenHandling_ThenLogsInformation()
    {
        // Given
        var request = new SampleRequestFaker().Generate();
        _delegate.Setup(n => n()).ReturnsAsync(1);

        // When
        var response = await _behavior.Handle(request, _delegate.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _delegate.Verify(handler => handler(), Times.Once);
        _logger.VerifyLog(LogLevel.Information, $"Handling {nameof(SampleRequest)}", Times.Once);
        _logger.VerifyLog(LogLevel.Information, $"Handled {nameof(SampleRequest)}", Times.Once);
    }

    [Fact]
    public async Task GivenLoggingBehaviorWithException_WhenHandling_ThenLogsError()
    {
        // Given
        var request = new SampleRequestFaker().Generate();
        _delegate.Setup(n => n()).ThrowsAsync(new Exception("Test Exception"));

        // When & Then
        await Assert.ThrowsAsync<Exception>(() => _behavior.Handle(request, _delegate.Object, CancellationToken.None));
        _delegate.Verify(handler => handler(), Times.Once);
        _logger.VerifyLog(LogLevel.Information, $"Handling {nameof(SampleRequest)}", Times.Once);
        _logger.VerifyLog(LogLevel.Error, $"Error while handling {nameof(SampleRequest)}", Times.Once);
    }
}