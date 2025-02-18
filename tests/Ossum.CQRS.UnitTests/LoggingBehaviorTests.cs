namespace Ossum.CQRS.UnitTests;

public class LoggingBehaviorTests
{
    private readonly TestRequestFaker _testRequestFaker = new();
    private readonly Mock<ILogger<Mediator>> _loggerMock = new();
    private readonly Mock<RequestHandlerDelegate<int>> _nextMock = new();
    private readonly LoggingBehavior<TestRequest, int> _behavior;

    public LoggingBehaviorTests()
    {
        _behavior = new LoggingBehavior<TestRequest, int>(_loggerMock.Object);
    }

    [Fact]
    public async Task GivenLoggingBehavior_WhenHandlingRequest_ThenLogsInformation()
    {
        // Given
        var request = _testRequestFaker.Generate();
        _nextMock.Setup(n => n()).ReturnsAsync(1);

        // When
        var response = await _behavior.Handle(request, _nextMock.Object, CancellationToken.None);

        // Then
        Assert.Equal(1, response);
        _nextMock.Verify(handler => handler(), Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, "Handling TestRequest", Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, "Handled TestRequest", Times.Once);
    }

    [Fact]
    public async Task GivenLoggingBehaviorWithException_WhenHandlingRequest_ThenLogsError()
    {
        // Given
        var request = _testRequestFaker.Generate();
        _nextMock.Setup(n => n()).ThrowsAsync(new Exception("Test Exception"));

        // When & Then
        await Assert.ThrowsAsync<Exception>(() => _behavior.Handle(request, _nextMock.Object, CancellationToken.None));
        _nextMock.Verify(handler => handler(), Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, "Handling TestRequest", Times.Once);
        _loggerMock.VerifyLog(LogLevel.Error, "Error while handling TestRequest", Times.Once);
    }
}