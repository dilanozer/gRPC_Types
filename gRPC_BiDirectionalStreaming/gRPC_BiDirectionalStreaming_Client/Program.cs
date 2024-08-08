using Grpc.Net.Client;
using gRPC_BiDirectionalStreaming_Client.Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5150");
var messageClient = new Message.MessageClient(channel);

var request = messageClient.SendMessage();

CancellationTokenSource cancellationTokenSource = new();

var task1 = Task.Run(async () =>
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(1000);

        request.RequestStream.WriteAsync(new MessageRequest
        {
            Name = "Dilan",
            Message = "Mesaj " + i
        });
    }
});

while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
{
    Console.WriteLine(request.ResponseStream.Current.Message);

}

await task1;

await request.RequestStream.CompleteAsync();
