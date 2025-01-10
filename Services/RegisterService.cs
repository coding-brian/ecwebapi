using AutoMapper;
using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class RegisterService(IMapper mapper, EcDbContext context)
    {
        private readonly IMapper _mapper = mapper;

        private readonly EcDbContext _context = context;

        public async Task<bool> RegisterAsync(MemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            member.Id = Guid.NewGuid();
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}