using Axxes.BullhornCRM.Extensions.DependencyInjection;
using Axxes.BullhornCRM.Models;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axxes.BullhornCRM.Tests.IntegrationTests;

public class UpdateTests
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateTests()
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
    
    [Fact]
    public async void QuerySpecialtiesEqualsTest()
    {
        var candidateProvider = _serviceProvider.GetRequiredService<IBullhorn<Candidate>>();
        var specialtyQueryProvider = _serviceProvider.GetRequiredService<IBullhornQuery<Specialty>>();
        var specialties = await specialtyQueryProvider.QueryIn(x => x.Name, new []{".NET Developer"}, orderBySelector: x => x.Name);
        var specialtiesArray = specialties.Data.ToArray();
        await candidateProvider.CreateToManyAssociations(55988, specialtiesArray, CancellationToken.None);
    }
}