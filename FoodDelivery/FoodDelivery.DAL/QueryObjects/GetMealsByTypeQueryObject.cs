using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetMealsByTypeQueryObject : EFCoreQueryObject<MealEntity>, IGetMealsByTypeQueryObject<MealEntity>
{
    private bool _isFilterUsed = false;

    public GetMealsByTypeQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<MealEntity> UseFilter(string mealType)
    {
        Filter(meal => meal.MealType.ToLower().Contains(mealType.ToLower()));
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<MealEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new Exception();

        return base.ExecuteAsync();
    }
}
