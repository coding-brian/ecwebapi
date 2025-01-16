﻿using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace EcWebapi.Services
{
    public class MemberService(IMapper mapper, TokenService tokenService, UnitOfWork unitOfWork, PayloadService payloadService)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        private readonly PasswordHasher<Member> _passwordHasher = new();

        private readonly TokenService _tokenService = tokenService;

        private readonly PayloadDto _payload = payloadService.GetPayload();

        public async Task<MemberDto> GetAsync()
        {
            return _mapper.Map<MemberDto>(await _unitOfWork.MemberRepository.GetAsync(member => member.Id == _payload.Id && member.EntityStatus));
        }

        public async Task<bool> CreateAsync(MemberDto dto)
        {
            try
            {
                var member = await _unitOfWork.MemberRepository.GetAsync(e => e.Phone == dto.Phone && e.EntityStatus);

                if (member != null) return false;

                var memberCaptcha = _unitOfWork.MemberCaptchaRepository.Get(captcha => captcha.Phone == dto.Phone && captcha.EntityStatus)
                                                                       .OrderByDescending(captcha => captcha.CreationTime)
                                                                       .FirstOrDefault();

                if (memberCaptcha.Code != dto.Captcha) return false;

                member = _mapper.Map<Member>(dto);
                member.Password = _passwordHasher.HashPassword(member, member.Password);
                member.IsActive = true;

                await _unitOfWork.MemberRepository.CreateAsync(member);

                await _unitOfWork.SaveChangesAsync();

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
                var member = await _unitOfWork.MemberRepository.GetAsync(e => e.Phone == dto.Account && e.IsActive);

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

        public async Task<string> CreateMemberCaptchaAsync(CreateCaptchaDto dto)
        {
            var member = await _unitOfWork.MemberRepository.GetAsync(e => e.Phone == dto.Phone && e.EntityStatus);

            if (member != null) return null;

            var timeDifference = DateTime.Now.AddSeconds(-30);

            var memberCaptchas = await _unitOfWork.MemberCaptchaRepository.GetListAsync(captcha => captcha.Phone == dto.Phone
                                                                                                   && captcha.CreationTime >= timeDifference
                                                                                                   && captcha.EntityStatus);
            if (memberCaptchas.Count > 0) return null;

            var random = new Random();
            string code = "";

            for (int i = 0; i < 4; i++)
            {
                int digit = random.Next(0, 10);
                code += digit.ToString();
            }

            await _unitOfWork.MemberCaptchaRepository.CreateAsync(new MemberCaptcha()
            {
                Code = code,
                Phone = dto.Phone,
            });

            await _unitOfWork.SaveChangesAsync();

            return code;
        }
    }
}