## Steps

> Step 1

```bash
dotnet new console -o grpc-client-sample
code -r GrpcGreeterClient
```

> Step 2

```bash
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
```

- Grpc.Net.Client, which contains the .NET Core client.
- Google.Protobuf, which contains protobuf message APIs for C#.
- Grpc.Tools, which contains C# tooling support for protobuf files. The tooling package isn't required at runtime, so the dependency is marked with PrivateAssets="All".

> Step 3

- Copy the Protos\greet.proto file from the gRPC Greeter service to the gRPC client project.

and add to csproj file below code block

```bash
<ItemGroup>
  <Protobuf Include="Protos\greet.proto" GrpcServices="Client" />
</ItemGroup>
```

> Step 4

```c#
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

            /*
            // For MacOS Configuration
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            */

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
```

## Notes:

You must apply the additional configuration for **MacOS**.

Please uncommit in above **Main** function **"For MacOS"** block.

and be carefull when you write the `GrpcChannel.ForAddress("http://localhost:5000");` code. You must write **http://** address(**not https**) and true port number.


## Resources

- https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-3.0&tabs=visual-studio-code
