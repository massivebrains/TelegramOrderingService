using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TelegramOrderingWebhook.Controllers;
using TelegramOrderingWebhook.Models;
using TelegramOrderingWebhook.Models.HttpRequests;
using TelegramOrderingWebhook.Models.HttpResponses;
using TelegramOrderingWebhook.Models.ServiceResponses;
using TelegramOrderingWebhook.Services;

namespace TelegramOrderingWebhook.Tests
{
    public class OrderControllerTest
    {
        private Mock<IOrderCalculationService> _orderCalculationServiceMock;

        [SetUp]
        public void Setup()
        {
            _orderCalculationServiceMock = new Mock<IOrderCalculationService>();

        }

        [Test]
        public void TestOrderShouldReturnInvalidMSISDNWhenNonNigerianPhoneNumberIsUsed()
        {
            OrderResponse expectedResponse = new OrderResponse {
                Status = "Failed",
                Message = AmazonShoppingTransformer.ERROR_INVALID_MSISDN,
                Data = null
            };

            _orderCalculationServiceMock.Setup(service => service.Calculate(It.IsAny<OrderRequest>()))
                .Returns(new OrderCalculationResponse
                {
                    Status = false,
                    ErrorCode = AmazonShoppingTransformer.INVALID_MSISDN,
                    Total = 0
                });

            OrderRequest request = new OrderRequest
            {
                LocationCode = "NG",
                PhoneNumber = "2338175020329",
                PaymentProviderCode = "OnePipe",
                Products = new List<ProductModel>
                {
                    new ProductModel{ ItemCode = "1002", Quantity = 1 }
                }
            };

            OrderController controller = new OrderController(_orderCalculationServiceMock.Object, new AmazonShoppingTransformer());

            IActionResult response = controller.Order(request);
            OkObjectResult okResult = response as OkObjectResult;
            OrderResponse returnedResponse = okResult.Value as OrderResponse;

            _orderCalculationServiceMock.Verify(service => service.Calculate(request), Times.Once());

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            Assert.AreEqual(expectedResponse.Status, returnedResponse.Status);
            Assert.AreEqual(expectedResponse.Message, returnedResponse.Message);
            Assert.AreEqual(expectedResponse.Data, returnedResponse.Data);

        }
    }
}
