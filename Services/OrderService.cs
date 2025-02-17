using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto.Order;
using EcWebapi.Enum;

namespace EcWebapi.Services
{
    public class OrderService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.Id = Guid.NewGuid();

            await _unitOfWork.OrderRepository.CreateAsync(order);

            var orderShipment = _mapper.Map<OrderShipment>(dto.Shipment);
            orderShipment.OrderId = order.Id;

            await _unitOfWork.OrderShipmentRepository.CreateAsync(orderShipment);

            var details = _mapper.Map<List<OrderDetail>>(dto.Details);
            foreach (var detail in details)
            {
                detail.Id = Guid.NewGuid();
                detail.OrderId = order.Id;
                await _unitOfWork.OrderDetailRepository.CreateAsync(detail);
            }

            if (dto.PaymentMethodType == PaymentMethodType.CreditCard)
            {
                var creditCard = _mapper.Map<OrderCreditCard>(dto.CreditCart);
                creditCard.OrderId = order.Id;

                await _unitOfWork.OrderCreditCardRepository.CreateAsync(creditCard);
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(_unitOfWork.OrderRepository.GetAsync(e => e.MemberId == dto.MemberId && e.Id == order.Id && e.EntityStatus));
        }
    }
}