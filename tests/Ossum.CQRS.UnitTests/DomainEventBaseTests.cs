namespace Ossum.CQRS.UnitTests;

public class DomainEventBaseTests
{
    [Fact]
    public void GivenValidProperties_WhenCreatingInstance_ThenValuesAreSetProperly()
    {
        // Given & When
        var domainEvent = new TestDomainEvent();

        // Then
        Assert.NotEqual(DateTime.MinValue, domainEvent.DateOccurred);
    }
}

