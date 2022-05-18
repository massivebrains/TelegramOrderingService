using System;
using System.Collections.Generic;
using System.Linq;
using TelegramOrderingWebhook.Models.HttpResponses;
using TelegramOrderingWebhook.Models.ServiceResponses;

namespace TelegramOrderingWebhook.Services
{
    public class AmazonShoppingTransformer: IOrderTransformer
    {
        public const string INVALID_MSISDN = "INVALID_MSISDN";
        public const string INVALID_PRODUCTS = "INVALID_PRODUCTS";
        public const string INVALID_QUANTITY = "INVALID_QUANTITY";
        public const string UK_DELIVERY_NOT_SUPPORTED = "UK_DELIVERY_NOT_SUPPORTED";


        public const string ERROR_INVALID_MSISDN = "Only Nigerian phone numbers are supported at the moment";
        public const string ERROR_INVALID_PRODUCTS = "One more more products are invalid";
        public const string ERROR_INVALID_QUANTITY = "One ore more prducts has an invalid quantity";
        public const string ERROR_UK_DELIVERY_NOT_SUPPORTED = "Delivery to UK is currently banned since they are moving mad";


        public OrderResponse transform(OrderCalculationResponse orderCalculationResponse)
        {
            if (orderCalculationResponse.Status)
            {
                return new OrderResponse
                {
                    Status = "Successful",
                    Message = "Order Calculated Successfully",
                    Data = new
                    {
                        Total = orderCalculationResponse.Total
                    }
                };
            }

            var ErrorCodeList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(INVALID_MSISDN, ERROR_INVALID_MSISDN),
                new KeyValuePair<string, string>(INVALID_PRODUCTS, ERROR_INVALID_PRODUCTS),
                new KeyValuePair<string, string>(INVALID_QUANTITY, ERROR_INVALID_QUANTITY),
                new KeyValuePair<string, string>(UK_DELIVERY_NOT_SUPPORTED, ERROR_UK_DELIVERY_NOT_SUPPORTED),
            };

            KeyValuePair<string, string> Error = ErrorCodeList.Where(row => row.Key.Equals(orderCalculationResponse.ErrorCode)).FirstOrDefault();

            return new OrderResponse
            {
                Status = "Failed",
                Message = Error.Value ?? "An Unexpected Error Occured",
                Data = null
            };
        }
    }
}
