using System;
using Nancy.Hosting.Self;

namespace NancyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://selfhost.nancyde.mo");
            using (var host = new NancyHost(uri))
            {
                host.Start();
                Console.WriteLine("Started Nancy self host");
                Console.WriteLine("Hit enter very softly to stop");
                Console.ReadLine();
            }
        }
    }
}
