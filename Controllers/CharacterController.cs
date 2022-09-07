using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Character;
using webapi.Services.CharacterService;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _charaterService;

        public CharacterController(ICharacterService charaterService)
        {
            _charaterService = charaterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> Get(){
            return Ok(await _charaterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRespones<GetCharacterDto>>> GetSingle(int id){
            var respones = await _charaterService.GetCharacterById(id);
            if(respones.Data == null){
                return NotFound(respones);
            }
            return Ok(respones);
        }

         [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> Delete(int id){
            var respones = await _charaterService.DeleteCharacter(id);
            if(respones.Data == null){
                return NotFound(respones);
            }
            return Ok(respones);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRespones<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter){
            return Ok(await _charaterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceRespones<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter){
            var respones = await _charaterService.UpdateCharacter(updateCharacter);
            if(respones.Data == null){
                return NotFound(respones);
            }
            return Ok(respones);
        }
    }
}