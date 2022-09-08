using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Dtos.Character;

namespace webapi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        // private static List<Character> characters = new List<Character>{
        //     new Character(),
        //     new Character {
        //         Id = 1,
        //         Name = "Ahmed",
        //         Class = RgbClass.Mage
        //     }
        // };

        public async Task<ServiceRespones<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceRespones = new ServiceRespones<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            //character.Id = characters.Max(c => c.Id) + 1;
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceRespones.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceRespones;
        }

        public async Task<ServiceRespones<List<GetCharacterDto>>> GetAllCharacters()
        {
            var respones = new ServiceRespones<List<GetCharacterDto>>();
            try
            {
                var dbCharacters = await _context.Characters.ToListAsync();
                respones.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (System.Exception ex)
            {
                respones.Success = false;
                respones.Message = ex.Message;
            }

            return respones;
        }

        public async Task<ServiceRespones<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceRespones = new ServiceRespones<GetCharacterDto>();
            var dbCharacters = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceRespones.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceRespones;
        }

        public async Task<ServiceRespones<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceRespones<GetCharacterDto> respones = new ServiceRespones<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                //_mapper.Map<Character>(updateCharacter);
                _mapper.Map(updateCharacter, character);
                await _context.SaveChangesAsync();
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
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                respones.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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