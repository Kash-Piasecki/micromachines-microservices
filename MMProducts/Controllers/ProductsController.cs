using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MMProducts.Data;
using MMProducts.Services;
using Newtonsoft.Json;

namespace MMProducts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsController(IProductsRepository productsRepository, IPublishEndpoint publishEndpoint)
        {
            _productsRepository = productsRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> Get()
        {
            return Ok(_productsRepository.GetAll().Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<ProductReadDto> Get(Guid id)
        {
            return Ok(_productsRepository.GetSingle(u => u.Id == id).AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create([FromBody] ProductUpsertDto productUpsertDto)
        {
            var product = productUpsertDto.AsProduct();
            var productCreationEvent = new ProductCreationEvent()
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            await _publishEndpoint.Publish(productCreationEvent);
            return Ok(_productsRepository.Add(product).AsDto());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<ProductReadDto> Update([FromBody] ProductUpsertDto productUpsertDto, Guid id)
        {
            var user = _productsRepository.GetSingle(x => x.Id == id).Edit(productUpsertDto);
            return Ok(_productsRepository.Edit(user).AsDto());
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult Remove(Guid id)
        {
            _productsRepository.Delete(_productsRepository.GetSingle(u => u.Id == id));
            return NoContent();
        }
    }
}