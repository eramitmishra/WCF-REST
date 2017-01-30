using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieRestFullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Imovieapi" in both code and config file together.
    [ServiceContract]
    public interface Imovieapi
    {
        /*Service at http://localhost/MovieRestFullService/movieapi.svc/GetMovieList */
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetMovieList")]
        List<Movie> getMovieList();
    }
}
