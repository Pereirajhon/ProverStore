using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Business.Notificacoes;
using ProverStore.Business.Services;
using Stripe;
using Stripe.Checkout;

namespace ProverStore.Api.Controllers
{
    [Authorize]
    public class CheckoutController : MainController
    {
        private ILogger<CheckoutController> _logger;
        private readonly StripeSettings _stripeSettings;
        private readonly IProdutoService _produtoService;
        private readonly IPedidoService _pedidoService;        
        public CheckoutController(IOptions<StripeSettings> stripeSettings,IProdutoService produtoService  ,IPedidoService pedidoService ,ILogger<CheckoutController> logger ,INotificador notificador) : base(notificador)
        {
            _produtoService = produtoService;
            _pedidoService = pedidoService;
            _stripeSettings = stripeSettings.Value;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/checkout")]
        public async Task<IActionResult> CreateCheckout(CarrinhoVM carrinho)
        {
            if (carrinho == null)
            {
                return CustomResponse();
            }

            var successUrl = "https://localhost:3000/sucesso";
            var cancelUrl = "https://localhost:3000/cancel";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;


            var lineItems = new List<SessionLineItemOptions>();

            foreach (var c in carrinho.CarrinhoItemsVM)
            {

                var produtoCarrinho = await _produtoService.ObterPorId(c.ProdutoId);

                if (produtoCarrinho == null) return CustomResponse();

                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "BRL",
                        UnitAmountDecimal = Convert.ToInt64(c.TotalProduto * 100), // Stripe usa centavos
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Metadata = new Dictionary<string, string>
                            {
                                { "produtoId", c.ProdutoId.ToString() },
                                { "clienteId", carrinho.ClienteId.ToString() }
                            },
                            Name = produtoCarrinho.Modelo,
                            Images = new List<string>
                            {
                                produtoCarrinho.ImagemProduto 
                            }
                        }
                    },
                    Quantity = c.Quantidade
                };

            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Metadata = new Dictionary<string, string>
                {
                    { "clienteId", carrinho.ClienteId.ToString() },
                    { "carrinhoId", carrinho.CarrinhoId.ToString() },
                    { "lineItemsData", SerializeLineItemsData(lineItems) }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);

            return CustomResponse(session.Url);
        }

        [HttpPost]
        [Route("api/stripe-webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _stripeSettings.WebHookSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent != null)
                    {
                        var lineItemsData = paymentIntent.Metadata["lineItemsData"];
                        var lineItems = DeserializeLineItemsData(lineItemsData);
                        var pedidoItemList = lineItems.Select(item => new PedidoItem
                        {
                            ProdutoId = Guid.Parse(item.PriceData.ProductData.Metadata["produtoId"]),
                            Quantidade = (int)(item.Quantity ?? 1),
                            Valor = Convert.ToDouble(item.PriceData.UnitAmountDecimal) / 100
                        }).ToList();

                        var pedido = new Pedido
                        {
                            ClienteId = Guid.Parse(paymentIntent.Metadata["clienteId"]),
                            DataPedido = DateTime.Now,
                            PedidoList = pedidoItemList,
                            TotalValor = pedidoItemList.Sum(p => p.Valor * p.Quantidade)
                        };

                        await _pedidoService.AddPedido(pedido);
                    }
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Implementar lógica de tratamento se necessário
                }
                else
                {
                    _logger.LogInformation("Evento não tratado: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError("Stripe Exception: {0}", e.Message);
                return BadRequest();
            }
        }



        /*
        [Authorize]
        [Route("api/checkout")]
        [HttpPost]
        public ActionResult CreateCheckout(CarrinhoVM carrinho)
        {
            if (carrinho == null)
                return CustomResponse();

            var successUrl = "https://localhost:3000/sucesso";
            var cancelUrl = "https://localhost:3000/cancel";
           // var domain = "http://localhost:4242";

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var c in carrinho.CarrinhoItemsVM)
            {
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        
                        Currency = "BRL",
                        UnitAmountDecimal = Convert.ToInt32(c.TotalProduto) ,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Metadata = new Dictionary<string, string>
                            {
                                {"produtoId", c.ProdutoId.ToString()},
                                {"clienteId", carrinho.ClienteId.ToString() },
                            },
                            Name = c.Produto.Modelo,
                            Images = new List<string> { c.Produto.ImagemProduto }
                        }
                    },
                    Quantity = c.Quantidade
                };
                lineItems.Add(lineItem);
            }
            

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Metadata = new Dictionary<string, string>
                {
                    { "lineItemsData", SerializeLineItemsData(lineItems) } 
                                
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);

            return CustomResponse(session.Url);
        }


        [Route("api/stripe-webhook")]
        [HttpPost]
        public async Task<ActionResult> WebHook(IOptions<StripeSettings> stripeOptions, HttpContext context)
        {
          //  var payload = await new StreamReader(context.Request.Body).ReadToEndAsync();
            /*
            try
            {
                var stripeEvent = EventUtility.ParseEvent(payload);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
            
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
         
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, _stripeSettings.WebHookSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent != null)
                    {
                        
                        var lineItemsData = paymentIntent.Metadata["lineItemsData"];

                        var lineItems = DeserializeLineItemsData(lineItemsData);
                        var pedidoItemList = new List<PedidoItem>();
                        foreach (var item in lineItems)
                        {
                           // var produtoImage = item.PriceData.ProductData.Images;
                          //  var nomeProduto = item.PriceData.ProductData.Name;
                            var quantidade = item.Quantity;
                            var valorUnitario = item.PriceData.UnitAmountDecimal;

                            var pedidoItem = new PedidoItem
                            {
                                
                                ProdutoId = Guid.Parse(paymentIntent.Metadata["produtoId"]),
                                Quantidade = Convert.ToInt32(quantidade),
                                Valor = Convert.ToDouble(valorUnitario)
                            };

                            pedidoItemList.Add(pedidoItem);
                        }
                        var pedido = new Pedido
                        {
                            ClienteId = Guid.Parse(paymentIntent.Metadata["clienteId"]),
                            DataPedido = DateTime.Now,
                            PedidoList = pedidoItemList,
                            TotalValor = pedidoItemList.Sum(p => p.Valor * p.Quantidade)
                        };

                        await _pedidoService.AddPedido(pedido);

                    }

                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            
        }
        */
        private string SerializeLineItemsData(List<SessionLineItemOptions> lineItems)
        {
            return JsonConvert.SerializeObject(lineItems);
        }
        private List<SessionLineItemOptions> DeserializeLineItemsData(string lineItemsData)
        {
            return JsonConvert.DeserializeObject<List<SessionLineItemOptions>>(lineItemsData);
        }
    }
    

}
