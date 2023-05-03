using Axxes.BullhornCRM.Extensions.DependencyInjection;
using Axxes.BullhornCRM.Models;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axxes.BullhornCRM.Tests.IntegrationTests;

public class FetchTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly int _dummyCandidateId;

    public FetchTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri("https://appconfig9c4fbe38.azconfig.io"), new AzureCliCredential());
            })
            .Build();
        
        var services = new ServiceCollection();
        
        services.AddBullhornCRM(options =>
        {
            options.ClientId = configuration["BULLHORN_CLIENTID"];
            options.ClientSecret = configuration["BULLHORN_CLIENTSECRET"];
            options.Username = configuration["BULLHORN_USERNAME"];
            options.Password = configuration["BULLHORN_PASSWORD"];
        });

        _dummyCandidateId = 48414;
        
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async void FetchTest()
    {
        var candidateProvider = _serviceProvider.GetRequiredService<IBullhorn<Candidate>>();
        var candidate = await candidateProvider.Get(_dummyCandidateId);
        Assert.NotNull(candidate);
    }

    [Fact]
    public async void FetchTest2()
    {
        var candidateProvider = _serviceProvider.GetRequiredService<IBullhorn<Candidate>>();
        var idList = new List<int> { _dummyCandidateId };
        var candidates = await candidateProvider.Get(idList);
        Assert.NotNull(candidates);
    }
    
    [Fact(Skip = "No subscriptions should be fetched during testing")]
    public async void FetchSubscriptions()
    {
        var subscriptionProvider = _serviceProvider.GetRequiredService<IBullhornSubscriptions>();
        var subscriptionResult = await subscriptionProvider.Get("30001");
        Assert.NotNull(subscriptionResult);
    }
}