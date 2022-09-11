using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Character;
using webapi.Services.CharacterService;

namespace webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _charaterService;

        public CharacterController(ICharacterService charaterService)
        {
            _charaterService = charaterService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> Get()
        {
            var respones = await _charaterService.GetAllCharacters();
            if (respones.Data?.Count == 0)
            {
                respones.Message = "No Found Data";
                return NotFound(respones);
            }
            return Ok(respones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRespones<GetCharacterDto>>> GetSingle(int id)
        {
            var respones = await _charaterService.GetCharacterById(id);
            if (respones.Data == null)
            {
                respones.Message = "not matched search criteria in a database";
                return NotFound(respones);
            }
            return Ok(respones);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> Delete(int id)
        {
            var respones = await _charaterService.DeleteCharacter(id);
            if (respones.Data == null)
            {
                return NotFound(respones);
            }
            return Ok(respones);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _charaterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceRespones<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var respones = await _charaterService.UpdateCharacter(updateCharacter);
            if (respones.Data == null)
            {
                return NotFound(respones);
            }
            return Ok(respones);
        }
    }
}