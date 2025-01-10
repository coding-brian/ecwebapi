using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Repository;

namespace EcWebapi.Services
{
    public class RegisterService(IMapper mapper, MemberRepository memberRepository)
    {
        private readonly IMapper _mapper = mapper;

        private readonly MemberRepository _memberRepository = memberRepository;

        public async Task<bool> RegisterAsync(MemberDto dto)
        {
            await _memberRepository.CreateAsync(_mapper.Map<Member>(dto));
            return true;
        }
    }
}