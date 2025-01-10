using AutoMapper;
using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class RegisterService
    {
        public RegisterService(IMapper mapper, EcDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        private readonly IMapper _mapper;

        private readonly EcDbContext _context;

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