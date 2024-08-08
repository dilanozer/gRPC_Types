using Grpc.Net.Client;
using gRPC_ClientStreaming_Client.Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5102");
var messageClient = new Message.MessageClient(channel);

var request = messageClient.SendMessage();

for (int i = 0; i < 10; i++)
{
    await Task.Delay(1000);

    await request.RequestStream.WriteAsync(new MessageRequest
    {
        Name = "Dilan",
        Message = "Mesaj " + i
    });
}

await request.RequestStream.CompleteAsync();

Console.WriteLine((await request.ResponseAsync).Message);
