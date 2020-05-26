using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpc_sample;

namespace grpc_client_sample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayMyNameAsync(
                  new MyNameRequest { MyName = "Aykut" });
            Console.WriteLine("Your Name: " + reply.YourName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
