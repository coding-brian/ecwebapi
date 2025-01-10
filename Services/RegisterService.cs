using AutoMapper;
using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Repository;

namespace EcWebapi.Services
{
    public class RegisterService(IMapper mapper, EcDbContext context)
    {
        private readonly IMapper _mapper = mapper;

        private readonly EcDbContext _context = context;

        private readonly MemberRepository _memberRepository;

        public async Task<bool> RegisterAsync(MemberDto dto)
        {
            await _memberRepository.CreateAsync(_mapper.Map<Member>(dto));
            return true;
        }
    }
}