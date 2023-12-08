using Axxes.BullhornCRM.Extensions.DependencyInjection;
using Axxes.BullhornCRM.Models;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axxes.BullhornCRM.Tests.IntegrationTests;

public class QueryTests
{
    private readonly IServiceProvider _serviceProvider;

    public QueryTests()
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


        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact(Skip = "Temporary disabling test")]
    public async void QuerySpecialtiesEqualsTest()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryEquals(x => x.Enabled, true, orderBySelector: x => x.Name);
        Assert.NotEmpty(specialties.Data);
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void QuerySpecialtiesEqualsTest2()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryEquals(x => x.Enabled, true);
        Assert.NotEmpty(specialties.Data);
    }

    [Fact(Skip = "Temporary disabling test")]
    public async void QuerySpecialtiesInTest()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryIn(x => x.Name, new []{"Back-End Developer", "Digital Marketeer"},
            orderBySelector: x => x.Name);
        Assert.NotEmpty(specialties.Data);
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void QuerySpecialtiesInTest2()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryIn(x => x.Name, new []{"Back-End Developer", "Digital Marketeer"});
        Assert.NotEmpty(specialties.Data);
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void QueryGreaterTest()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryGreater(x => x.Id, 0);
        Assert.NotEmpty(specialties.Data);
    }
    
    [Fact(Skip = "Temporary disabling test")]
    public async void QuerySmallerTest()
    {
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QuerySmaller(x => x.Id, 2000202, count: 10);
        Assert.NotEmpty(specialties.Data);
        Assert.True(specialties.Data.All(x => x.Id < 2000202));
    }
}