using FoodDelivery.BL.Tests.Handlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.UserModels;

namespace FoodDelivery.BL.Tests;

public class UserTestEntity
{
    public CustomerEntity CustomerEntity { get; set; }
    public UserCreateModel UserCreateModel { get; set; }
    public UserDetailModel UserDetailModel { get; set; }
    public UserListModel UserListModel { get; set; }
    public UserUpdateModel UserUpdateModel { get; set; }

    public List<PaymentCardTestEntity> PaymentCards
    {
        set
        {
            CustomerEntity.PaymentCards = value.Select(p => p.PaymentCardEntity).ToList();
            
        }
    }
    
    public List<FeedbackTestEntity> Feedbacks
    {
        set
        {
            CustomerEntity.Feedbacks = value.Select(f => f.FeedbackEntity).ToList();
        }
    }
    
    public List<OrderTestEntity> Orders
    {
        set
        {
            CustomerEntity.Orders = value.Select(o => o.OrderEntity).ToList();
        }
    }

    public UserTestEntity(int id, string name, string surname, AddressTestEntity address)
    {
        // CustomerEntity = new CustomerEntity
        // {
        //     Id = id, Name = name, Surname = surname,
        //     Address = address.AddressEntity, AddressId = address.AddressEntity.Id
        // };

        UserCreateModel = new UserCreateModel();

        UserDetailModel = new UserDetailModel();

        UserListModel = new UserListModel();

        UserUpdateModel = new UserUpdateModel();
    }
}