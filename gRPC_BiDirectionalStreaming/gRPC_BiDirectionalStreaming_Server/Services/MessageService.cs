﻿using System;
using Grpc.Core;
using gRPC_BiDirectionalStreaming_Server.Protos;

namespace gRPC_BiDirectionalStreaming_Server.Services
{
	public class MessageService : Message.MessageBase
	{
		private readonly ILogger<MessageService> _logger;
		public MessageService(ILogger<MessageService> logger)
		{
			_logger = logger;
		}

        public override async Task SendMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
			var task1 = Task.Run(async () =>
			{
				while (await requestStream.MoveNext(context.CancellationToken))
				{
					Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
				}
			});

			for (int i = 0; i < 10; i++)
			{
				await Task.Delay(1000);

				await responseStream.WriteAsync(new MessageResponse
				{
					Message = "Mesaj " + i
				});
			}

            await task1;
        }
    }
}

