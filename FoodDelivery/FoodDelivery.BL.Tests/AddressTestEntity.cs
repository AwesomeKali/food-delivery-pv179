using Castle.DynamicProxy;
using FoodDelivery.BL.Tests.Handlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Tests;

public class AddressTestEntity
{
    public AddressEntity AddressEntity { get; set; }
    public AddressCreateModel AddressCreateModel { get; set; }
    public AddressDetailModel AddressDetailModel { get; set; }
    public AddressListModel AddressListModel { get; set; }
    public AddressUpdateModel AddressUpdateModel { get; set; }

    public List<OrderTestEntity> Orders
    {
        set
        {
            AddressEntity.Orders = value.Select(o => o.OrderEntity).ToList();
        }
    }

    public AddressTestEntity(int id, string city, string street, string number, string postalCode)
    {
        AddressEntity = new AddressEntity
            {
                Id = id, Street = street, Number = number, City = city, PostalCode = postalCode,
            };
        
        AddressCreateModel = new AddressCreateModel
        {
            Street = street, Number = number, City = city, PostalCode = postalCode
        };
        
        AddressDetailModel = new AddressDetailModel
        {
            Id = id, Street = street, Number = number, City = city, PostalCode = postalCode,
        };
        AddressListModel = new AddressListModel
        {
            Id = id, Street = street, Number = number, City = city, PostalCode = postalCode
        }; 
        AddressUpdateModel = new AddressUpdateModel
        {
            Id = id, Street = street, Number = number, City = city, PostalCode = postalCode
        };
    }
}