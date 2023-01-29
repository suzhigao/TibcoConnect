using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibcoConnect;

namespace TibcoConnect
{
    class Publisher
    {
        public void Run()
        {
            TIBCO.Rendezvous.Environment.Open();
            var subject = "ME.TEST";
            var network = "192.168.xx.xxx";
            var port = "7500";
            var transport = new NetTransport(port, network, port);
            Console.WriteLine("Server running..");
            Console.WriteLine("Press x to exit or any other key to send message");
            while (true)
            {
                var m = new Message();
                m.SendSubject = subject;
                m.AddField("Test", "TestValue");
                transport.Send(m);
                var line = Console.ReadLine();
                if (line.ToUpper().Equals("X")) break;
            }
            TIBCO.Rendezvous.Environment.Close();
        }
        static void Main(string[] args)
        {
            new Publisher().Run();
        }
    }
}
