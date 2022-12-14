using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;
public class GetOrdersByRestaurantId : EFCoreQueryObject<OrderEntity>, IGetOrdersByRestaurantId<OrderEntity>
{
    private bool _isFilterUsed = false;

    public GetOrdersByRestaurantId(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<OrderEntity> UseFilter(int restaurantId)
    {
        Filter(order => order.RestaurantId == restaurantId);
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<OrderEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new Exception();

        return base.ExecuteAsync();
    }
}