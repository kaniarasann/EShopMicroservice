using DiscountGRPC.Data;
using DiscountGRPC.Models;
using DiscountGRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Services
{
    public class DiscountService(DiscountContext dbcontext,ILogger<DiscountService> logger):DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var req = request.Coupon.Adapt<Coupons>();
            if (req == null)
                throw new RpcException(new Status(statusCode: StatusCode.InvalidArgument, "Invalid request details"));
            await dbcontext.Coupon.AddAsync(req);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount have been saved successfully Product Name : {name} and amount :{amount}", req.ProductName, req.Amount);            

            return req.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await dbcontext.Coupon.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (result == null)
                throw new RpcException(new Status(statusCode: StatusCode.InvalidArgument, "Invalid request details"));

            dbcontext.Coupon.Remove(result);

            await dbcontext.SaveChangesAsync();

            return new DeleteDiscountResponse { IsSuccess = true };

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await dbcontext.Coupon.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (result == null)
            {
                result = new Coupons { Id = 0 , Amount = 0 ,ProductName = string.Empty,ProductDescription = string.Empty };
            }
            
            logger.LogInformation("Discount is retrived for product name : {productname} and discount amount {amount}", result.ProductName, result.Amount);

            return result.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var req = request.Coupon.Adapt<Coupons>();
            if (req == null)
                throw new RpcException(new Status(statusCode: StatusCode.InvalidArgument, "Invalid request details"));
            dbcontext.Coupon.Update(req);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount have been updated successfully Product Name : {name} and amount :{amount}", req.ProductName, req.Amount);

            return req.Adapt<CouponModel>();
        }
    }
}
