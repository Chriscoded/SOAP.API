using System.Text;
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

    #region WSDL Handling
    [HttpGet]
    public IActionResult Get(string? wsdl, string? xsd)
    {
        var controllerName = ControllerContext.RouteData.Values["controller"]?.ToString();

        if(wsdl is not null)
            return ProcessWsdlFile($"~/wsdl/{(controllerName is null ? "" : controllerName + "/" )}{(wsdl == string.Empty ? "Service.wsdl" : $"{wsdl}.wsdl")}");

        if(xsd is not null)
        {
            if(xsd == string.Empty)
            {
                //TODO should be SOAPFault
                return BadRequest("xsd parameter cannot be empty");
            }
            else
            {
                return ProcessWsdlFile($"~/wsdl/{(controllerName is null ? "" : controllerName + "/" )}{xsd}.xml");
            }
           
            
        }
         //TODO should be SOAPFault
        return BadRequest("xsd parameter cannot be empty");
    }

    private IActionResult ProcessWsdlFile(string path)
    {
        var _baseURL = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        //Convert virtual path to physical
        if(path.StartsWith("~"))
            path.Replace("~", _env.ContentRootPath);
        String content;
        try
        {
            content = System.IO.File.ReadAllText(path, Encoding.UTF8);
        }
        catch
        {
            return new ObjectResult("wsdl not found") {StatusCode = StatusCodes.Status500InternalServerError};
        }
        content.Replace("{SERVICEURL}", _baseURL);
        return File(Encoding.UTF8.GetBytes(content), "text/xml; charset=UTF-8");
    }
    #endregion
}
