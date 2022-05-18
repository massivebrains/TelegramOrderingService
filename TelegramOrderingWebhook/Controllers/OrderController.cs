using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelegramOrderingWebhook.Models.HttpRequests;
using TelegramOrderingWebhook.Models.HttpResponses;
using TelegramOrderingWebhook.Models.ServiceResponses;
using TelegramOrderingWebhook.Services;

namespace TelegramOrderingWebhook.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderCalculationService _orderCalculationService;
        private readonly IOrderTransformer _orderTransformer;

        public OrderController(IOrderCalculationService orderCalculationService, IOrderTransformer orderTransformer)
        {
            _orderCalculationService = orderCalculationService;
            _orderTransformer = orderTransformer;
        }

        [HttpPost]
        public IActionResult Order([FromBody] OrderRequest orderRequest)
        {
            try
            {
                // Validate orderRequest [Skipped for simplicity]

                OrderCalculationResponse orderCalculationResponse = _orderCalculationService.Calculate(orderRequest);
                OrderResponse response = _orderTransformer.transform(orderCalculationResponse);

                return Ok(response);

            } catch (Exception e)
            {
                OrderResponse response = new OrderResponse
                {
                    Status = "Failed",
                    Message = e.Message,
                    Data = null
                };

                return BadRequest(response);
            }
            
        }

    }
}
