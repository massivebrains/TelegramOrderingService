using System;
using TelegramOrderingWebhook.Models.HttpRequests;
using TelegramOrderingWebhook.Models.ServiceResponses;

namespace TelegramOrderingWebhook.Services
{
    public interface IOrderCalculationService
    {
        public OrderCalculationResponse Calculate(OrderRequest orderRequest);
    }
}
