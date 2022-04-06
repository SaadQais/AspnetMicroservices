using Basket.API.GrpcServices.Interfaces;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices.Services
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscountAsync(string productName)
        {
             return await _discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
        }
    }
}
