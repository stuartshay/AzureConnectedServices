using AzureConnectedServices.Services.Proto;
using AzureConnectedServices.GrpcService.Services;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MapsterMapper;

namespace AzureConnectedServices.Services.ProtoFirst;

/// <summary>
/// Noaa Climate Data Service
/// </summary>
public class NoaaClimateDataServiceGrpc : NoaaWeather.NoaaWeatherBase
{

    private readonly INoaaClimateDataService _noaaClimateDataService;

    private readonly IMapper _mapper;

    public NoaaClimateDataServiceGrpc(INoaaClimateDataService noaaClimateDataService, IMapper mapper, ILogger<NoaaClimateDataServiceGrpc> logger)
    {
        _noaaClimateDataService = noaaClimateDataService;
        _mapper = mapper;
    }
    
    public override async Task<NoaaClimateDataResponse> NoaaWeatherOperation(NoaaClimateDataRequest request, ServerCallContext context)
    {
        var mappedRequest = _mapper.Map<Models.Client.NoaaClimateDataRequest>(request);

        var response = await _noaaClimateDataService.GetClimateData(mappedRequest);

        var results = new RepeatedField<Result>();
        foreach (var item in response.Results)
        { 
            //TODO FIX DATE
            var x = _mapper.Map<Proto.Result>(item);
            x.Date = Timestamp.FromDateTime(DateTime.SpecifyKind(item.Date, DateTimeKind.Utc));

            results.Add(x);
        }

        var responseMapped = _mapper.Map<Proto.NoaaClimateDataResponse>(response);
        responseMapped.Result.AddRange(results);

        return responseMapped;
    }

}
