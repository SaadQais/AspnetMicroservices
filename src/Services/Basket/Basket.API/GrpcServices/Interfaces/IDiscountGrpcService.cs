using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices.Interfaces
{
    public interface IDiscountGrpcService
    {
        Task<CouponModel> GetDiscountAsync(string productName);
    }
}
