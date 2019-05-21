using System;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var webServer = new WebServer("http://localhost:1234/");
            webServer.Run();
            Console.WriteLine("Press key to finish!");
            webServer.Stop();
        }
    }
}
