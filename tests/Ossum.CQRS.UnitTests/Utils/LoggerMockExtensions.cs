namespace Ossum.CQRS.UnitTests.Utils;

public static class LoggerMockExtensions
{
    public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times)
    {
        loggerMock.Verify(l => l.Log(
            level,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(message)),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), times);
    }

    public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Func<Times> times)
    {
        loggerMock.VerifyLog(level, message, times());
    }
}
