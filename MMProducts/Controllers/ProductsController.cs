using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMProducts.Data;
using MMProducts.Services;

namespace MMProducts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
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
        public ActionResult<ProductReadDto> Create([FromBody] ProductUpsertDto productUpsertDto)
        {
            var product = productUpsertDto.AsProduct();
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