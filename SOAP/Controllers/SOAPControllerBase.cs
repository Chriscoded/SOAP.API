using Microsoft.AspNetCore.Mvc;
using SOAP.Model;
namespace SOAP.Controllers;

[ApiController]
[Route("[controller]")]
public class SOAPControllerBase : ControllerBase
{
    private readonly ILogger<SOAPControllerBase> _logger;
    private readonly IWebHostEnvironment _env;
    protected SOAPVersion SOAPVersion { get; set; }
    public SOAPControllerBase(ILogger<SOAPControllerBase> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;

        SOAPControllerAttribute? soapversionAttribute = Attribute.GetCustomAttribute(GetType(), typeof(SOAPControllerAttribute)) as SOAPControllerAttribute;
        if (soapversionAttribute is null)
            throw new Exception("class deriving from SOAPControllerBase is Missing SOAPController attribute");
        else
            SOAPVersion = soapversionAttribute.SOAPVersion;
    }

    public virtual SOAPResponseEnvelope CreateSOAPResponseEnvelope() => SOAPVersion == SOAPVersion.v1_1 ? new SOAP1_1ResponseEnvelope() : new SOAP1_2ResponseEnvelope();
}