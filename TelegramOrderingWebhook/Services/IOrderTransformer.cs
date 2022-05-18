using System;
using TelegramOrderingWebhook.Models.HttpResponses;
using TelegramOrderingWebhook.Models.ServiceResponses;

namespace TelegramOrderingWebhook.Services
{
    public interface IOrderTransformer
    {
        public OrderResponse transform(OrderCalculationResponse orderCalculationResponse);
    }
}
