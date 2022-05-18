using System;
using System.Collections.Generic;

namespace TelegramOrderingWebhook.Models.HttpRequests
{
    public class OrderRequest
    {
        public string LocationCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentProviderCode { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
