using System;
using System.Threading.Tasks;
using CommonLibrary.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MMBasket.Clients;
using MMBasket.Data;
using MMBasket.Services;

namespace MMBasket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ProductsClient _productsClient;
        private readonly UsersClient _usersClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository, ProductsClient productsClient,
            UsersClient usersClient, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _productsClient = productsClient;
            _usersClient = usersClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<BasketReadDto>> Get(Guid userId)
        {
            var baskets = _basketRepository.GetAll(x => x.UserId == userId && x.IsDone == false);
            var user = await _usersClient.GetUser(userId);
            var productsList = await _productsClient.GetProductsList();
            var basketReadDto = Extensions.GetBasketReadDto(baskets, productsList, user);
            return Ok(basketReadDto);
        }

        [HttpPost]
        public async Task<ActionResult<BasketReadDto>> Post(BasketCreateDto basketCreateDto)
        {
            var basket = _basketRepository.GetSingle(
                item => item.UserId == basketCreateDto.UserId && item.ProductId == basketCreateDto.ProductId);

            if (basket is null)
            {
                basket = new Basket()
                {
                    Id = Guid.NewGuid(),
                    ProductId = basketCreateDto.ProductId,
                    UserId = basketCreateDto.UserId,
                    Quantity = basketCreateDto.Quantity,
                    IsDone = false
                };

                _basketRepository.Add(basket);
            }
            else
            {
                basket.Quantity += basketCreateDto.Quantity;
                _basketRepository.Edit(basket);
            }

            var basketUpdateEvent = new BasketUpdateEvent()
            {
                ProductId = basketCreateDto.ProductId,
                Quantity = basketCreateDto.Quantity
            };
            await _publishEndpoint.Publish(basketUpdateEvent);
            var baskets = _basketRepository.GetAll(x => x.UserId == basketCreateDto.UserId && x.IsDone == false);
            var user = await _usersClient.GetUser(basketCreateDto.UserId);
            var productsList = await _productsClient.GetProductsList();
            var basketReadDto = Extensions.GetBasketReadDto(baskets, productsList, user);
            return Ok(basketReadDto);
        }
    }
}