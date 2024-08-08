using Grpc.Net.Client;
using gRPC_Unary_Client.Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5137");
var messageClient = new Message.MessageClient(channel);

MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest
{
    Message = "Merhaba",
    Name = "Dilan"
});

Console.WriteLine(response.Message);

