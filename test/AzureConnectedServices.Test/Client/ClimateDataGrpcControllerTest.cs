using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureConnectedServices.Core.Configuration;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;

namespace AzureConnectedServices.Test.Client;

public class ClimateDataGrpcControllerTest
{



    //private ClimateDataGrpcController GetClimateDataGrpcController(
    //    //ICatService catService = null,
    //    IMapper mapper = null,
    //    ILogger<ClimateDataGrpcController> logger = null,
    //    IOptionsSnapshot<Settings> settings = null
    //)
    //{
    //    //catService ??= new Mock<ICatService>().Object;
    //    //fileService ??= new Mock<IFileService>().Object;
    //    logger ??= new Mock<ILogger<ClimateDataGrpcController>>().Object;

    //    //var env = new Mock<IWebHostEnvironment>();
    //    //env.Setup(m => m.ContentRootPath).Returns("/");

    //    //settings ??= _serviceProvider.GetService<IOptions<ApplicationOptions>>();

    //    return new ClimateDataGrpcController(logger, mapper, null, settings); //(catService, fileService, logger, env.Object, settings);
    //}





}
