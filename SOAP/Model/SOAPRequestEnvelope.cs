using System.Xml.Serialization;
namespace SOAP.Model
{
    [XmlRoot("Envelope", Namespace = SOAPConstants.SOAP1_1Namespace)]
    public partial class SOAP1_1RequestEnvelope : SOAPResponseEnvelope
    {
        
    }

    [XmlRoot("Envelope", Namespace = SOAPConstants.SOAP1_2Namespace)]
    public partial class SOAP1_2RequestEnvelope : SOAPResponseEnvelope
    {
        
    }

    public partial class SOAPRequestEnvelope
    {
        public SOAPHeader? Header { get; set; }
        public SOAPRequestBody? Body { get; set; }
    }
}