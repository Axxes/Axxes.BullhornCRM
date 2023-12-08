using Axxes.BullhornCRM.Extensions.DependencyInjection;
using Axxes.BullhornCRM.Models;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axxes.BullhornCRM.Tests.IntegrationTests;

public class FindTests
{
    private readonly ServiceProvider _serviceProvider;

    public FindTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri("https://appconfig23a9a9e4.azconfig.io"), new AzureCliCredential());
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


        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void FindAllCandidatesTest()
    {
        var candidateSearch = _serviceProvider.GetRequiredService<IBullhornSearch<Candidate>>();
        var candidates = await candidateSearch.FindAll(new Dictionary<string, string>()
        {
            {
                "firstName", "Ruben"
            }
        });
        Assert.NotEmpty(candidates.Data);
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void FindCandidatesTest()
    {
        var candidateSearch = _serviceProvider.GetRequiredService<IBullhornSearch<Candidate>>();
        var candidate = await candidateSearch.Find(new Dictionary<string, string>()
        {
            {
                "firstName", "Ruben"
            },
            {
                "lastName", "Vervust"
            },
        });
        Assert.NotNull(candidate);
    }
}