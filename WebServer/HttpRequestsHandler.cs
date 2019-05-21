using System;
using System.Net;
using WebServer.Interfaces;

namespace WebServer
{
    public class HttpRequestsHandler : IHttpRequestsHandler
    {
        private HttpListenerContext _context;
        private HttpListenerRequest _request;

        public HttpRequestsHandler(HttpListenerContext context)
        {
            _context = context;
            _request = context.Request;
        }

        public void HandleGetRequest()
        {
            try
            {               
                var acceptHeader = _request.Headers.GetValues("Accept");
                var hostHeader = _request.Headers.GetValues("Host");               
                var getRequestHandler = new GetRequestHandler(_context);
                getRequestHandler.SendHttpResponse(_request.RawUrl);
            }
            catch (Exception ex)
            {

                throw;
            }

        }       

        public void HandlePostRequest()
        {
            throw new System.NotImplementedException();
        }

        public void HandlePutRequest()
        {
            throw new System.NotImplementedException();
        }

        public void HandleDeleteRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}