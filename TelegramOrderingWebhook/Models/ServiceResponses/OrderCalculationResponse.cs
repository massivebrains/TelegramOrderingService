using System;
namespace TelegramOrderingWebhook.Models.ServiceResponses
{
    public class OrderCalculationResponse
    {
        public bool Status { get; set; }
        public string ErrorCode { get; set; }
        public Decimal Total { get; set; }
    }
}
