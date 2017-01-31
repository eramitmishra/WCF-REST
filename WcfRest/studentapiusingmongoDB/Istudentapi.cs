using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace studentapiusingmongoDB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Istudentapi" in both code and config file together.
    [ServiceContract]
    public interface Istudentapi
    {
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, 
            Method = "GET",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate ="getdata")]
        string DoWork();

        //http://localhost/studentapiusingmongoDB/studentapi.svc/getstudents
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "GET",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getstudents")]
        Task<string> getstudents();


    }
}
