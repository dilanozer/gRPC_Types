using Grpc.Net.Client;
using gRPC_ServerStreaming_Client.Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5293");
var messageClient = new Message.MessageClient(channel);

var response = messageClient.SendMessage(new MessageRequest
{
    Message = "Merhaba",
    Name = "Dilan"
});

CancellationTokenSource cancellationTokenSource = new();

while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
{
    Console.WriteLine(response.ResponseStream.Current.Message);
}

