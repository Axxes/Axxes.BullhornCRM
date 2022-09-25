using Axxes.BullhornCRM.Extensions.DependencyInjection;
using Axxes.BullhornCRM.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Axxes.BullhornCRM.Tests.IntegrationTests;

public class DependencyInjectionTests
{
    private readonly IServiceProvider _serviceProvider;

    public DependencyInjectionTests()
    {
        var services = new ServiceCollection();
        services.AddBullhornCRM(x =>
        {

        });

        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Theory]
    [MemberData(nameof(WhenRegisteringBullhornCRM_AllDefaultEntitiesShouldBeRegistered_Data))]
    public void WhenRegisteringBullhornCRM_AllDefaultEntitiesShouldBeRegistered(Type expectedType)
    {
        var service = _serviceProvider.GetService(expectedType);
        
        Assert.NotNull(service);
    }
    
    public static IEnumerable<object[]> WhenRegisteringBullhornCRM_AllDefaultEntitiesShouldBeRegistered_Data =>
        new List<object[]>
        {
            new object[] { typeof(IBullhorn<Candidate>) },
            new object[] { typeof(IBullhorn<ClientCorporation>) },
            new object[] { typeof(IBullhorn<Contact>) },
            new object[] { typeof(IBullhorn<JobSubmission>) },
            new object[] { typeof(IBullhorn<Note>) },
            new object[] { typeof(IBullhorn<Placement>) },
            new object[] { typeof(IBullhorn<PlacementChangeRequest>) },
            new object[] { typeof(IBullhorn<Shortlist>) },
            new object[] { typeof(IBullhorn<ShortlistCandidate>) },
        };
    
    [Theory]
    [MemberData(nameof(WhenRegisteringBullhornCRM_AllHistoryEntitiesShouldBeRegistered_Data))]
    public void WhenRegisteringBullhornCRM_AllHistoryEntitiesShouldBeRegistered(Type expectedType)
    {
        var service = _serviceProvider.GetService(expectedType);
        
        Assert.NotNull(service);
    }
    
    public static IEnumerable<object[]> WhenRegisteringBullhornCRM_AllHistoryEntitiesShouldBeRegistered_Data =>
        new List<object[]>
        {
            new object[] { typeof(IBullhornEditHistory<Candidate>) }
        };
}