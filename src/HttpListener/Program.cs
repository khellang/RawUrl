using System;
using Common;
using Microsoft.Owin.Hosting;

namespace HttpListener
{
    public static class Program
    {
        private const string Url = @"http://localhost:1337";

        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(Url))
            {
                Console.WriteLine("Listening at '{0}'...", Url);
                Console.ReadLine();
            }
        }
    }
}
