using System;
using System.IO;
using System.ServiceModel;
using Xunit;

namespace WcfClient
{
    public class Program
    {
        static string host;
        static int exitcode;
        static string logpath;

        static void Main(string[] args)
        {
            host = Environment.GetEnvironmentVariable("host") ?? "localhost";
            exitcode = 0;
            logpath = args[0];

            CallViaHttp();
            CallViaNetTcp();
            Environment.Exit(exitcode);           
        }

        static void CallViaHttp()
        {
            try
            {
                var address = string.Format("http://{0}/Service1.svc", host);
                var binding = new BasicHttpBinding();
                var factory = new ChannelFactory<IService1>(binding, address);
                var channel = factory.CreateChannel();
            
                Assert.Equal("Hello WCF from Container!", channel.Hello("WCF"));
            }
            catch (Exception ex) {
                ExceptionLogging(ex);
                exitcode = int.MinValue;
            }
        }

        static void CallViaNetTcp()
        {
            try
            {
                var address = string.Format("net.tcp://{0}/Service1.svc", host);
                var binding = new NetTcpBinding(SecurityMode.None);
                var factory = new ChannelFactory<IService1>(binding, address);
                var channel = factory.CreateChannel();
            
                Assert.Equal("Hello WCF from Container!", channel.Hello("WCF"));
            }
            catch (Exception ex)
            {
                ExceptionLogging(ex);
                exitcode = int.MinValue;
            }
        }

        static void ExceptionLogging(Exception ex)
        {
            string path = logpath;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("=======================================");
            }
        }
    }
}
