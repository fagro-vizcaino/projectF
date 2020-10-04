using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Components.Common
{
    public class FMessage: IFMessage
    {
        private readonly MessageService _service;

        public FMessage(MessageService messageService)
            => (_service) = messageService;

        public async Task Success(string message, int duration)
            => await _service.Success(message, duration);
    }

    public interface IFMessage
    {
        Task Success(string message, int duration);
    }
}
