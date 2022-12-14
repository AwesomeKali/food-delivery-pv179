using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetOrderItemsByOrderIdQueryObject : EFCoreQueryObject<OrderItemEntity>, IGetOrderItemsByOrderIdQueryObject<OrderItemEntity>
{
    private bool _isFilterUsed = false;

    public GetOrderItemsByOrderIdQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<OrderItemEntity> UseFilter(int orderId)
    {
        Filter(o => o.OrderId == orderId);
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<OrderItemEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new Exception();

        return base.ExecuteAsync();
    }
}