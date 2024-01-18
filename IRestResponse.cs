using System.Net;

namespace ArgaamSchedular
{
    internal interface IRestResponse
    {
        HttpStatusCode StatusCode { get; }
        string Content { get; set; }
    }
}