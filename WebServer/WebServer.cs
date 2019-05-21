using System;
using System.Net;
using System.Threading.Tasks;

namespace WebServer
{
    public class WebServer
    {
        private HttpListener _listener;
        private string _prefix;               

        public WebServer(string prefix)
        {
            _listener = new HttpListener();
            _prefix = prefix;          
        }

        public void Run()
        {
            if (string.IsNullOrEmpty(_prefix))
                throw new ArgumentException("prefix");

            _listener.Prefixes.Add(_prefix);
            _listener.Start();

            Console.WriteLine("Listening...");
            while (_listener.IsListening)
            {
                Task.Factory.StartNew((c) =>
                {
                    var context = c as HttpListenerContext;
                    try
                    {                       
                        var httpRequestsHandler = new HttpRequestsHandler(context);
                        switch (context.Request.HttpMethod)
                        {
                            case "GET":
                                httpRequestsHandler.HandleGetRequest();
                                break;
                            case "POST":
                                httpRequestsHandler.HandlePostRequest();
                                break;
                            case "PUT":
                                httpRequestsHandler.HandlePutRequest();
                                break;
                            case "DELETE":
                                httpRequestsHandler.HandleDeleteRequest();
                                break;
                            default:
                                break;
                        }                    
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }, _listener.GetContext());
            }
        }

       

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
