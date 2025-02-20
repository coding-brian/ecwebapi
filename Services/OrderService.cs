using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Dto.Order;
using EcWebapi.Dto.OrderDetail;
using EcWebapi.Enum;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EcWebapi.Services
{
    public class OrderService(IMapper mapper, UnitOfWork unitOfWork, PayloadService payloadService)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        private readonly PayloadDto _payload = payloadService.GetPayload();

        public async Task<OrderDto> GetAsync(Guid orderId)
        {
            var order = _mapper.Map<OrderDto>(await _unitOfWork.OrderRepository.GetAsync(e => e.MemberId == _payload.Id && e.Id == orderId && e.EntityStatus));
            if (order == null) return null;

            order.Details = new List<OrderDetailDto>();

            var details = await _unitOfWork.OrderDetailRepository.GetQuerable(q => q.OrderId == order.Id && q.EntityStatus)
                .Join(_unitOfWork.ProductRepository.GetQuerable(p => p.EntityStatus),
                detail => detail.ProductId,
                product => product.Id,
                (detail, product) => new { Detail = detail, Product = product })
                .ToListAsync();

            var images = _mapper.Map<List<ProductImageDto>>(await _unitOfWork.ProductImageRepository.GetListAsync(image => details.Select(d => d.Detail.ProductId).Contains(image.ProductId) && image.IsActive && image.EntityStatus)).OrderBy(dto => dto.Priority);

            foreach (var detail in details)
            {
                var detailDto = _mapper.Map<OrderDetailDto>(detail.Detail);

                detailDto.Product = _mapper.Map<OrderDetailProductDto>(detail.Product);
                detailDto.Product.Images = images.Where(image => image.ProductId == detailDto.Product.Id).ToList();

                order.Details.Add(detailDto);
            }

            return order;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            try
            {
                var order = _mapper.Map<Order>(dto);
                order.Id = Guid.NewGuid();
                order.MemberId = _payload.Id.Value;

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

                return await GetAsync(order.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return null;
            }
        }
    }
}