using System;
using Grpc.Core;
using gRPC_ClientStreaming_Server.Protos;

namespace gRPC_ClientStreaming_Server.Services
{
	public class MessageService : Message.MessageBase
	{
		private readonly ILogger<MessageService> _logger;
		public MessageService(ILogger<MessageService> logger)
		{
			_logger = logger;
		}

        public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
			while (await requestStream.MoveNext(context.CancellationToken))
			{
				Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
			}

			return new MessageResponse
			{
				Message = "Veri alınmıştır..."
			};
        }
    }
}

