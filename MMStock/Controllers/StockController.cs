using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMStock.Data;
using MMStock.Serivces;

namespace MMStock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<StockReadDto>> Get()
        {
            return Ok(_stockRepository.GetAll().Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<StockReadDto> Get(Guid id)
        {
            return Ok(_stockRepository.GetSingle(u => u.Id == id).AsDto());
        }

        [HttpPost]
        public ActionResult<StockReadDto> Create([FromBody] StockUpsertDto stockUpsertDto)
        {
            var stock = stockUpsertDto.AsStock();
            return Ok(_stockRepository.Add(stock).AsDto());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<StockReadDto> Update([FromBody] StockUpsertDto stockUpsertDto, Guid id)
        {
            var stock = _stockRepository.GetSingle(x => x.Id == id).Edit(stockUpsertDto);
            return Ok(_stockRepository.Edit(stock).AsDto());
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult Remove(Guid id)
        {
            _stockRepository.Delete(_stockRepository.GetSingle(u => u.Id == id));
            return NoContent();
        }
    }
}