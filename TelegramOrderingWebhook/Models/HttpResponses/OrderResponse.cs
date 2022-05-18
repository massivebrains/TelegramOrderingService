using System;
namespace TelegramOrderingWebhook.Models.HttpResponses
{
    public class OrderResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
