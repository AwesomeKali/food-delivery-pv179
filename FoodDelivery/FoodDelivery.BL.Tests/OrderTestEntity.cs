using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Tests.Handlers;

public class OrderTestEntity
{
    public OrderEntity OrderEntity { get; set; }
    public OrderCreateModel OrderCreateModel { get; set; }
    public OrderDetailModel OrderDetailModel { get; set; }
    public OrderListModel OrderListModel { get; set; }

    public List<OrderItemTestEntity> OrderItems
    {
        set
        {
            OrderEntity.OrderItems = value.Select(o => o.OrderItemEntity).ToList();
            OrderDetailModel.OrderItems = value.Select(o => o.OrderItemListModel).ToList();
            OrderListModel.OrderItems = value.Select(o => o.OrderItemListModel).ToList();
        }
    }

    public OrderTestEntity(int id, PaymentType paymentType, UserTestEntity user, AddressTestEntity address)
    {
        OrderEntity = new OrderEntity
        {
            Id = id, PaymentType = paymentType,
            User = user.CustomerEntity, UserId = user.CustomerEntity.Id,
            Address = address.AddressEntity, AddressId = address.AddressEntity.Id,
        };

        // OrderCreateModel = new OrderCreateModel
        // {
        //     PaymentType = paymentType,
        //     UserId = user.CustomerEntity.Id,
        //     AddressId = address.AddressEntity.Id,
        // };

        OrderDetailModel = new OrderDetailModel
        {
            Id = id, PaymentType = paymentType,
            User = user.UserDetailModel, UserId = user.CustomerEntity.Id,
            Address = address.AddressDetailModel, AddressId = address.AddressEntity.Id,
        };

        OrderListModel = new OrderListModel
        {
            Id = id, PaymentType = paymentType,
            UserId = user.CustomerEntity.Id,
            AddressId = address.AddressEntity.Id,
        };
    }
}