using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Interfaces
{
    public interface IHttpRequestsHandler
    {
        void HandleGetRequest();
        void HandlePostRequest();
        void HandlePutRequest();
        void HandleDeleteRequest();
    }
}
