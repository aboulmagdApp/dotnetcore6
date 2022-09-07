using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Character;

namespace webapi.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceRespones<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceRespones<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceRespones<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceRespones<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task<ServiceRespones<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}