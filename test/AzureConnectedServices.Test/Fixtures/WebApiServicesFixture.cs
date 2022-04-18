using System.Reflection;
using AzureConnectedServices.WebApi.Mappings;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AzureConnectedServices.Test.Fixtures;

public class WebApiServicesFixture : IDisposable
{
    public IMapper Mapper { get; }
        
    public WebApiServicesFixture()
    {
        var services = new ServiceCollection();

        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetAssembly(typeof(NoaaClimateDataMappings)));

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
            
        var sp = services.BuildServiceProvider();
        Mapper = sp.GetRequiredService<IMapper>();
    }

    public void Dispose()
    {
    }
}
