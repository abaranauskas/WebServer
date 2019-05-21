using System;
using System.IO;
using System.Net;

namespace WebServer
{
    public class GetRequestHandler
    {
        private HttpListenerContext _context;
        private HttpListenerResponse _response;

        public GetRequestHandler(HttpListenerContext context)
        {
            _context = context;
            _response = context.Response;
        }

        public HttpStatusCode SendHttpResponse(string rawUrl)
        {
            try
            {
                HttpStatusCode responseCode;

                if (rawUrl == "/")
                    responseCode = SendOkResponse(new FileReadResults());
                else
                {
                    var fileHandler = new FileHandler(rawUrl);
                    responseCode = fileHandler.IsFileNameValid ? SendOkResponse(fileHandler.GetFileReadResults()) : SendBadRequestRespone();
                }

                return responseCode;
            }
            catch (FileNotFoundException) { return SendNotFoundRespone(); }
            catch (DirectoryNotFoundException) { return SendBadRequestRespone(); }
            finally
            {
                _context.Response.OutputStream.Close();
                _response.Close();
            }
        }

        private HttpStatusCode SendNotFoundRespone()
        {
            _response.StatusCode = (int)HttpStatusCode.NotFound;
            _context.Response.OutputStream.Flush();
            return HttpStatusCode.NotFound;
        }

        private HttpStatusCode SendBadRequestRespone()
        {
            _response.StatusCode = (int)HttpStatusCode.BadRequest;
            _context.Response.OutputStream.Flush();
            return HttpStatusCode.BadRequest;
        }



        private HttpStatusCode SendOkResponse(FileReadResults fileReadResults)
        {

            _response.StatusCode = (int)HttpStatusCode.OK;
            _response.Headers.Add(HttpResponseHeader.ContentType, GetContentTypeByFileExtension(fileReadResults.FileExtension));
            _response.Headers.Add(HttpResponseHeader.Date, DateTime.Now.ToLongDateString());

            var buffer = fileReadResults.FileBytes;
            _context.Response.ContentLength64 = buffer.Length;
            _context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            return HttpStatusCode.OK;
        }

        private string GetContentTypeByFileExtension(string fileExtension)
        {
            string result;
            switch (fileExtension.ToLower())
            {
                case ".txt":
                    result = "text/plain";
                    break;
                case ".png":
                    result = "image/png";
                    break;
                default:
                    result = "text/html";
                    break;

            }
            return result;
        }

    }
}