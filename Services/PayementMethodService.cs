using AutoMapper;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class PayementMethodService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<List<PaymentMethodDto>> GetListAsync()
        {
            return _mapper.Map<List<PaymentMethodDto>>(await _unitOfWork.PaymentMethodRepository.GetListAsync(e => e.EntityStatus));
        }
    }
}