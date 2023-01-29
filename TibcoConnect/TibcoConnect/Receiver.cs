using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using TIBCO.Rendezvous;

namespace TibcoConnect
{
    class Receiver
    {
        public void Run()
        {
            TIBCO.Rendezvous.Environment.Open();
            var subject = "ME.TEST";
            var network = "192.168.xx.xxx";
            var port = "7500";
            Transport transport = new NetTransport(port, network, port);
            Listener listener = new Listener(
                    Queue.Default,
                    transport,
                    subject,
                    new object()
                    );
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            var dispacher = new Dispatcher(listener.Queue);
            dispacher.Join();
            Console.WriteLine("Client running..");
            Console.ReadKey();
            TIBCO.Rendezvous.Environment.Close();
        }
        void listener_MessageReceived(object listener, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            Console.WriteLine(messageReceivedEventArgs.Message.GetField("Test").Value);
        }
        static void Main(string[] args)
        {
            new Receiver().Run();
        }
    }
}
