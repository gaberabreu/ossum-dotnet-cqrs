namespace Ossum.CQRS.UnitTests;

public class DomainEventBaseTests
{
    [Fact]
    public void GivenParameters_WhenInstantiating_ThenHasCorrectValues()
    {
        // Given & When
        var domainEvent = new SampleDomainEvent();

        // Then
        Assert.NotEqual(DateTime.MinValue, domainEvent.DateOccurred);
    }
}

