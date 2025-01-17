using AutoMapper;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class ApiResponseService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponseDto<T>> GetErrorAsync<T>(string code)
        {
            return new ApiResponseDto<T>()
            {
                IsSuccess = false,
                Error = _mapper.Map<ApiResponseErrorDto>(await _unitOfWork.ApiResponseRepository.GetAsync(e => e.Code == code && e.EntityStatus))
            };
        }
    }
}