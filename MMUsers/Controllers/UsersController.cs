using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMUsers.Data;
using MMUsers.Services;

namespace MMUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> Get()
        {
            return Ok(_usersRepository.GetAll().Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<UserReadDto> Get(Guid id)
        {
            return Ok(_usersRepository.GetSingle(u => u.Id == id).AsDto());
        }

        [HttpPost]
        public ActionResult<UserReadDto> Create([FromBody] UserUpsertDto userUpsertDto)
        {
            var user = userUpsertDto.AsUser();
            return Ok(_usersRepository.Add(user).AsDto());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<UserReadDto> Update([FromBody] UserUpsertDto userUpsertDto, Guid id)
        {
            var user = _usersRepository.GetSingle(x => x.Id == id).Edit(userUpsertDto);
            return Ok(_usersRepository.Edit(user).AsDto());
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult Remove(Guid id)
        {
            _usersRepository.Delete(_usersRepository.GetSingle(u => u.Id == id));
            return NoContent();
        }
    }
}