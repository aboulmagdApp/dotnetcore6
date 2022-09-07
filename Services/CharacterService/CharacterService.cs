using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using webapi.Dtos.Character;

namespace webapi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {
                Id = 1,
                Name = "Ahmed",
                Class = RgbClass.Mage
            }
        };

        public async Task<ServiceRespones<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceRespones = new ServiceRespones<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceRespones.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceRespones;
        }

        public async Task<ServiceRespones<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceRespones<List<GetCharacterDto>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
        }

        public async Task<ServiceRespones<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceRespones = new ServiceRespones<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceRespones.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceRespones;
        }

        public async Task<ServiceRespones<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceRespones<GetCharacterDto> respones = new ServiceRespones<GetCharacterDto>();
            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
                //_mapper.Map<Character>(updateCharacter);
                _mapper.Map(updateCharacter, character);
                respones.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {

                respones.Success = false;
                respones.Message = ex.Message;
            }
            return respones;
        }

        public async Task<ServiceRespones<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var respones = new ServiceRespones<List<GetCharacterDto>>();
            try
            {
                Character character = characters.First(c => c.Id == id);
                characters.Remove(character);
                respones.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (System.Exception ex)
            {

                respones.Success = false;
                respones.Message = ex.Message;
            }
            return respones;
        }
    }
}