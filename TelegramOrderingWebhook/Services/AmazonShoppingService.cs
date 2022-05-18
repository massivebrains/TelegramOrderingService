using System;
using System.Linq;
using TelegramOrderingWebhook.Models.HttpRequests;
using TelegramOrderingWebhook.Models.ServiceResponses;

namespace TelegramOrderingWebhook.Services
{
    public class AmazonShoppingService: IOrderCalculationService
    {
        public const string NIGERIAN_MSISDN_PREFIX = "234";
        public const string UK = "UK";

        public OrderCalculationResponse Calculate(OrderRequest orderRequest)
        {
            // Do some API request to space or whatever. But for now, make we run am.

            OrderCalculationResponse response = new OrderCalculationResponse();

            try
            {
                if (orderRequest.PhoneNumber != null && orderRequest.PhoneNumber.Substring(0, 3) != NIGERIAN_MSISDN_PREFIX)
                {
                    throw new Exception("INVALID_MSISDN");
                }

                if (orderRequest.Products.Count == 0)
                {
                    throw new Exception("INVALID_PRODUCTS");
                }

                if (orderRequest.Products.Where(product => product.Quantity < 1).FirstOrDefault() != null)
                {
                    throw new Exception("INVALID_QUANTITY");
                }

                if(orderRequest.LocationCode != null && orderRequest.LocationCode == UK)
                {
                    throw new Exception($"{UK}_DELIVERY_NOT_SUPPORTED");
                }

                response.Status = true;
                response.ErrorCode = null;
                response.Total = new Random().Next(10000, 50000);

            } catch (Exception e)
            {
                response.Status = false;
                response.ErrorCode = e.Message;
                response.Total = 0;
            }

            return response;
            
        }
    }
}
