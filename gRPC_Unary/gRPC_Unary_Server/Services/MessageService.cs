using System;
using Grpc.Core;
using gRPC_Unary_Server.Protos;

namespace gRPC_Unary_Server.Services
{
	public class MessageService : Message.MessageBase
	{
        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} | Name: {request.Name} ");

            return new MessageResponse
            {
                Message = "Mesaj başarıyla alınmıştır..."
            };
        }
    }
}

