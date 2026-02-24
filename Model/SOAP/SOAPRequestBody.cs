using System.Xml.Serialization;
namespace  SOAP.Model;

[XmlType(Namespace = SOAPRequestBody.DefaultNamespace)]
public partial class SOAPRequestBody
{
    public const string DefaultNamespacePrefix = "ser";
    public const string DefaultNamespace = "http://some.com/service/";

    public GetWeatherForecastRequest? GetWeatherForcast {get; set;}
}