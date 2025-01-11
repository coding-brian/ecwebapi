using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Repository;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace EcWebapi.Services
{
    public class MemberService(IMapper mapper, MemberRepository memberRepository, IConfiguration configuration, TokenService tokenService, PayloadService payloadService)
    {
        private readonly IMapper _mapper = mapper;

        private readonly MemberRepository _memberRepository = memberRepository;

        private readonly PasswordHasher<Member> _passwordHasher = new PasswordHasher<Member>();

        private readonly IConfiguration _configuration = configuration;

        private readonly TokenService _tokenService = tokenService;

        private readonly PayloadService _payloadService = payloadService;

        public async Task<MemberDto> GetAsync()
        {
            var payload = _payloadService.GetPayload();

            return _mapper.Map<MemberDto>(await _memberRepository.GetAsync(member => member.Id == payload.Id && member.EntityStatus));
        }

        public async Task<bool> RegisterAsync(MemberDto dto)
        {
            try
            {
                var member = _mapper.Map<Member>(dto);
                member.Password = _passwordHasher.HashPassword(member, member.Password);

                await _memberRepository.CreateAsync(member);
                await _memberRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception:");
                return false;
            }
        }

        public async Task<TokenDto> Login(LoginDto dto)
        {
            try
            {
                var member = await _memberRepository.GetAsync(e => e.Phone == dto.Account);

                if (member == null) return null;

                var result = _passwordHasher.VerifyHashedPassword(member, member.Password, dto.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return _tokenService.GetAccessToken(member);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception:");
                return null;
            }
        }
    }
}