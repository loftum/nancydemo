using System;
using Nancy.Hosting.Self;

namespace NancyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://selfhost.nancyde.mo")))
            {
                host.Start();
                Console.WriteLine("Started Nancy self host");
                Console.WriteLine("Hit enter very softly to stop");
                Console.ReadLine();
            }
        }
    }
}
