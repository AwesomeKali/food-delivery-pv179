using AutoMapper;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.Repositories.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.Repositories.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers;

public class HandlerFixture
{
    public Mock<IMapper> MapperMock { get; set; }

    public Mock<IUnitOfWorkProvider<IEFCoreUnitOfWork>> UnitOfWorkProviderMock { get; set; }
    public Mock<IEntityRepository<AddressEntity>> AddressRepositoryMock { get; set; }
    public Mock<IEntityRepository<CustomerEntity>> UserRepositoryMock { get; set; }

    public Mock<IEntityRepository<RestaurantEntity>> RestaurantRepositoryMock { get; set; }

    public Mock<IEntityRepository<PaymentCardEntity>> PaymentCardRepositoryMock { get; set; }

    public Mock<IEntityRepository<OrderItemEntity>> OrderItemRepositoryMock { get; set; }

    public Mock<IEntityRepository<OrderEntity>> OrderRepositoryMock { get; set; }

    public Mock<IEntityRepository<MealEntity>> MealRepositoryMock { get; set; }

    public Mock<IEntityRepository<FeedbackEntity>> FeedbackRepositoryMock { get; set; }
    public List<AddressTestEntity> Addresses { get; set; }
    public List<FeedbackTestEntity> Feedbacks { get; set; }
    public List<MealTestEntity> Meals { get; set; }
    public List<OrderItemTestEntity> OrderItems { get; set; }
    public List<OrderTestEntity> Orders { get; set; }
    public List<PaymentCardTestEntity> PaymentCards { get; set; }
    public List<RestaurantTestEntity> Restaurants { get; set; }
    public List<UserTestEntity> Users { get; set; }

    public HandlerFixture()
    {
        MapperMock = new Mock<IMapper>();

        UnitOfWorkProviderMock = new Mock<IUnitOfWorkProvider<IEFCoreUnitOfWork>>();
        UnitOfWorkMock = new Mock<IEFCoreUnitOfWork>();
        AddressRepositoryMock = new Mock<IEntityRepository<AddressEntity>>();
        FeedbackRepositoryMock = new Mock<IEntityRepository<FeedbackEntity>>();
        MealRepositoryMock = new Mock<IEntityRepository<MealEntity>>();
        OrderRepositoryMock = new Mock<IEntityRepository<OrderEntity>>();
        OrderItemRepositoryMock = new Mock<IEntityRepository<OrderItemEntity>>();
        PaymentCardRepositoryMock = new Mock<IEntityRepository<PaymentCardEntity>>();
        RestaurantRepositoryMock = new Mock<IEntityRepository<RestaurantEntity>>();
        UserRepositoryMock = new Mock<IEntityRepository<CustomerEntity>>();

        Addresses = new List<AddressTestEntity>
        {
            new(1, "New York", "My Drive", "3818", "10016"),
            new(2, "New York", "Briercliff Road", "3435", "10019"),
            new(3, "New York", "Layman Court", "1079", "10001"),
            new(4, "New York", "Farnum Road", "3521", "10011"),
            new(5, "New York", "My Drive", "676", "10013"),
            new(6, "New York", "Oakwood Avenue", "1702", "10011"),
            new(7, "New York", "Duncan Avenue", "2187", "10014"),
            new(8, "New York", "Old Dear Lane", "2952", "10013"),
            new(9, "New York", "Settlers Lane", "3872", "10007"),
            new(10, "New York", "Geraldine Lane", "1264", "10007")
        };

        Users = new List<UserTestEntity>
        {
            new(1, "Danika", "Benjamin", Addresses[0]),
            new(2, "Melinda", "Greer", Addresses[1]),
            new(3, "Alya", "Atkinson", Addresses[2]),
            new(4, "Jordanna", "Alford", Addresses[3]),
            new(5, "Paolo", "Hatfield", Addresses[4]),
            new(6, "Rebekah", "Mora", Addresses[5]),
            new(7, "Antoni", "Ryan", Addresses[6]),
            new(8, "Emelia", "Fritz", Addresses[7]),
            new(9, "Gia", "Connelly", Addresses[8]),
            new(10, "Hermione", "Galindo", Addresses[9])
        };

        Restaurants = new List<RestaurantTestEntity>
        {
            new(1, "The Private Port"),
            new(2, "The Sailing Stranger"),
            new(3, "The Juniper Angel"),
            new(4, "The Mountain Courtyard"),
            new(5, "The Mountain Lantern"),
            new(6, "Indigo"),
            new(7, "The Peacock"),
            new(8, "Sapphire"),
            new(9, "The Nightingale"),
            new(10, "Mumbles")
        };

        Meals = new List<MealTestEntity>
        {
            new(1,
                "Broccoli and pancetta fusilli",
                "Fresh egg pasta in a sauce made from fresh broccoli and smoked pancetta",
                10.5,
                "Pasta",
                Restaurants[0]),
            new(2,
                "Steak and parmesan bagel",
                "A warm bagel filled with steak and parmesan",
                15.20,
                "Steak",
                Restaurants[1]),
            new(3,
                "Tuna and lemon penne",
                "Fresh egg tubular pasta in a sauce made from tuna and tangy lemon",
                8.60,
                "Pasta",
                Restaurants[2]),
            new(4,
                "Sausage and chilli burger",
                "Succulent burger made from chunky sausage and spicy chilli, served in a roll",
                16.50,
                "Burger",
                Restaurants[3]
            ),
            new(5,
                "Grouse and lettuce bagel",
                "A warm bagel filled with grouse and romaine lettuce",
                5.10,
                "Bagel",
                Restaurants[4]
            ),
            new(
                6,
                "Tofu and squash sausages",
                "Sizzling sausages made from smoked tofu and pattypan squash, served in a roll",
                7.10,
                "Vegan",
                Restaurants[5]
            ),
            new(7,
                "Strawberry and pepper soup",
                "Fresh strawberries and sweet pepper combined into smooth soup",
                8.00,
                "Soup",
                Restaurants[6]
            ),
            new(8,
                "Sole and durian salad",
                "Sole and fresh durian served on a bed of lettuce",
                10.50,
                "Salad",
                Restaurants[7]
            ),
            new(9,
                "Pasta salad with garlic dressing",
                "A mouth-watering pasta salad served with garlic dressing",
                11.30,
                "Pasta salad",
                Restaurants[8]
            ),
            new(10,
                "Pesto and spinach pasta",
                "Fresh egg pasta in a sauce made from green pesto and baby spinach",
                8.00,
                "Pasta",
                Restaurants[9]
            )
        };

        PaymentCards = new List<PaymentCardTestEntity>
        {
            new(1, "4556606818982609", "8/2028", "926", Users[0]),
            new(2, "4024007112838247", "7/2025", "874", Users[1]),
            new(3, "4024007151953246", "4/2026", "303", Users[2]),
            new(4, "4716973905394149", "5/2023", "451", Users[3]),
            new(5, "4916527759652115", "12/2023", "797", Users[4]),
            new(6, "5536939805025518", "7/2022", "902", Users[5]),
            new(7, "5234119467692641", "2/2028", "508", Users[6]),
            new(8, "5528132118567844", "2/2025", "775", Users[7]),
            new(9, "5486714790391591", "5/2024", "913", Users[8]),
            new(10, "5272279698145299", "5/2023", "794", Users[9]),
        };

        Orders = new List<OrderTestEntity>
        {
            new(1, PaymentType.Card, Users[0], Addresses[0]),
            new(2, PaymentType.Card, Users[1], Addresses[1]),
            new(3, PaymentType.Card, Users[2], Addresses[2]),
            new(4, PaymentType.Card, Users[3], Addresses[3]),
            new(5, PaymentType.Card, Users[4], Addresses[4]),
            new(6, PaymentType.Cash, Users[5], Addresses[5]),
            new(7, PaymentType.Cash, Users[6], Addresses[6]),
            new(8, PaymentType.Cash, Users[7], Addresses[7]),
            new(9, PaymentType.Cash, Users[8], Addresses[8]),
            new(10, PaymentType.Coupon, Users[9], Addresses[9]),
        };

        OrderItems = new List<OrderItemTestEntity>
        {
            new(1, Meals[0].MealEntity.Price, 2, Orders[0], Meals[0]),
            new(2, Meals[2].MealEntity.Price, 3, Orders[0], Meals[2]),
            new(3, Meals[1].MealEntity.Price, 1, Orders[1], Meals[1]),
            new(4, Meals[2].MealEntity.Price, 1, Orders[1], Meals[2]),
            new(5, Meals[3].MealEntity.Price, 2, Orders[1], Meals[3]),
            new(6, Meals[0].MealEntity.Price, 1, Orders[2], Meals[0]),
            new(7, Meals[9].MealEntity.Price, 1, Orders[2], Meals[9]),
            new(8, Meals[6].MealEntity.Price, 5, Orders[3], Meals[6]),
            new(9, Meals[4].MealEntity.Price, 2, Orders[4], Meals[4]),
            new(10, Meals[8].MealEntity.Price, 4, Orders[5], Meals[8]),
            new(11, Meals[6].MealEntity.Price, 1, Orders[6], Meals[6]),
            new(12, Meals[7].MealEntity.Price, 4, Orders[7], Meals[7]),
            new(13, Meals[8].MealEntity.Price, 3, Orders[8], Meals[8]),
            new(14, Meals[5].MealEntity.Price, 1, Orders[8], Meals[5]),
            new(15, Meals[0].MealEntity.Price, 1, Orders[9], Meals[0]),
        };

        Feedbacks = new List<FeedbackTestEntity>
        {
            new(1, 1, "Too bad... Didn't like it at all.", Meals[0], new RestaurantTestEntity(-1, ""), Users[0]),
            new(2, 2, "Eh...", Meals[1], new RestaurantTestEntity(-1, ""), Users[1]),
            new(3, 3, "Not great, not terrible.", Meals[2], new RestaurantTestEntity(-1, ""), Users[0]),
            new(4, 4, "Liked it a lot!!", Meals[3], new RestaurantTestEntity(-1, ""), Users[1]),
            new(5, 5, "Awesome! Best thing I ever eaten!", Meals[4], new RestaurantTestEntity(-1, ""), Users[4]),
            new(6, 5, "Awesome! Best thing I ever eaten!", Meals[5], new RestaurantTestEntity(-1, ""), Users[8]),
            new(7, 4, "Liked it a lot!!", Meals[6], new RestaurantTestEntity(-1, ""), Users[6]),
            new(8, 3, "Not great, not terrible.", Meals[7], new RestaurantTestEntity(-1, ""), Users[7]),
            new(9, 2, "Eh...", Meals[8], new RestaurantTestEntity(-1, ""), Users[8]),
            new(10, 1, "Too bad... Didn't like it at all.", Meals[9], new RestaurantTestEntity(-1, ""), Users[2]),
            new(11, 1, "Really bad place.", new MealTestEntity(-1, "", "", -1, "", Restaurants[0]), Restaurants[0],
                Users[0]),
            new(12, 2, "Won't come again...", new MealTestEntity(-1, "", "", -1, "", Restaurants[1]), Restaurants[1],
                Users[1]),
            new(13, 3, "Could have been better.", new MealTestEntity(-1, "", "", -1, "", Restaurants[2]),
                Restaurants[2], Users[0]),
            new(14, 4, "Great place!", new MealTestEntity(-1, "", "", -1, "", Restaurants[3]), Restaurants[3],
                Users[1]),
            new(15, 5, "My favorite place! I highly recommend!", new MealTestEntity(-1, "", "", -1, "", Restaurants[4]),
                Restaurants[4], Users[4]),
            new(16, 5, "My favorite place! I highly recommend!", new MealTestEntity(-1, "", "", -1, "", Restaurants[5]),
                Restaurants[5], Users[8]),
            new(17, 4, "Great place!", new MealTestEntity(-1, "", "", -1, "", Restaurants[6]), Restaurants[6],
                Users[6]),
            new(18, 3, "Could have been better.", new MealTestEntity(-1, "", "", -1, "", Restaurants[7]),
                Restaurants[7], Users[7]),
            new(19, 2, "Won't come again...", new MealTestEntity(-1, "", "", -1, "", Restaurants[8]), Restaurants[8],
                Users[8]),
            new(20, 1, "Really bad place.", new MealTestEntity(-1, "", "", -1, "", Restaurants[9]), Restaurants[9],
                Users[2]),
        };

        Addresses[0].Orders = new List<OrderTestEntity> { Orders[0] };
        Addresses[1].Orders = new List<OrderTestEntity> { Orders[1] };
        Addresses[2].Orders = new List<OrderTestEntity> { Orders[2] };
        Addresses[3].Orders = new List<OrderTestEntity> { Orders[3] };
        Addresses[4].Orders = new List<OrderTestEntity> { Orders[4] };
        Addresses[5].Orders = new List<OrderTestEntity> { Orders[5] };
        Addresses[6].Orders = new List<OrderTestEntity> { Orders[6] };
        Addresses[7].Orders = new List<OrderTestEntity> { Orders[7] };
        Addresses[8].Orders = new List<OrderTestEntity> { Orders[8] };
        Addresses[9].Orders = new List<OrderTestEntity> { Orders[9] };

        Users[0].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[0] };
        Users[1].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[1] };
        Users[2].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[2] };
        Users[3].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[3] };
        Users[4].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[4] };
        Users[5].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[5] };
        Users[6].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[6] };
        Users[7].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[7] };
        Users[8].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[8] };
        Users[9].PaymentCards = new List<PaymentCardTestEntity> { PaymentCards[9] };

        Users[0].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[0], Feedbacks[2], Feedbacks[10], Feedbacks[12] };
        Users[1].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[1], Feedbacks[11], Feedbacks[13] };
        Users[2].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[9], Feedbacks[19] };
        Users[3].Feedbacks = new List<FeedbackTestEntity> { };
        Users[4].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[4], Feedbacks[14] };
        Users[5].Feedbacks = new List<FeedbackTestEntity> { };
        Users[6].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[6], Feedbacks[16] };
        Users[7].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[7] };
        Users[8].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[5], Feedbacks[8], Feedbacks[15], Feedbacks[18] };
        Users[9].Feedbacks = new List<FeedbackTestEntity> { };

        Users[0].Orders = new List<OrderTestEntity> { Orders[0] };
        Users[1].Orders = new List<OrderTestEntity> { Orders[1] };
        Users[2].Orders = new List<OrderTestEntity> { Orders[2] };
        Users[3].Orders = new List<OrderTestEntity> { Orders[3] };
        Users[4].Orders = new List<OrderTestEntity> { Orders[4] };
        Users[5].Orders = new List<OrderTestEntity> { Orders[5] };
        Users[6].Orders = new List<OrderTestEntity> { Orders[6] };
        Users[7].Orders = new List<OrderTestEntity> { Orders[7] };
        Users[8].Orders = new List<OrderTestEntity> { Orders[8] };
        Users[9].Orders = new List<OrderTestEntity> { Orders[9] };

        Restaurants[0].Meals = new List<MealTestEntity> { Meals[0] };
        Restaurants[1].Meals = new List<MealTestEntity> { Meals[1] };
        Restaurants[2].Meals = new List<MealTestEntity> { Meals[2] };
        Restaurants[3].Meals = new List<MealTestEntity> { Meals[3] };
        Restaurants[4].Meals = new List<MealTestEntity> { Meals[4] };
        Restaurants[5].Meals = new List<MealTestEntity> { Meals[5] };
        Restaurants[6].Meals = new List<MealTestEntity> { Meals[6] };
        Restaurants[7].Meals = new List<MealTestEntity> { Meals[7] };
        Restaurants[8].Meals = new List<MealTestEntity> { Meals[8] };
        Restaurants[9].Meals = new List<MealTestEntity> { Meals[9] };

        Restaurants[0].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[10] };
        Restaurants[1].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[11] };
        Restaurants[2].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[12] };
        Restaurants[3].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[13] };
        Restaurants[4].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[14] };
        Restaurants[5].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[15] };
        Restaurants[6].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[16] };
        Restaurants[7].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[17] };
        Restaurants[8].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[18] };
        Restaurants[9].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[19] };

        Meals[0].OrderItems = new List<OrderItemTestEntity> { OrderItems[0], OrderItems[5], OrderItems[14] };
        Meals[1].OrderItems = new List<OrderItemTestEntity> { OrderItems[2] };
        Meals[2].OrderItems = new List<OrderItemTestEntity> { OrderItems[1], OrderItems[3] };
        Meals[3].OrderItems = new List<OrderItemTestEntity> { OrderItems[4] };
        Meals[4].OrderItems = new List<OrderItemTestEntity> { OrderItems[8] };
        Meals[5].OrderItems = new List<OrderItemTestEntity> { OrderItems[13] };
        Meals[6].OrderItems = new List<OrderItemTestEntity> { OrderItems[7], OrderItems[10] };
        Meals[7].OrderItems = new List<OrderItemTestEntity> { OrderItems[11] };
        Meals[8].OrderItems = new List<OrderItemTestEntity> { OrderItems[9], OrderItems[12] };
        Meals[9].OrderItems = new List<OrderItemTestEntity> { OrderItems[6] };

        Meals[0].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[0] };
        Meals[1].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[1] };
        Meals[2].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[2] };
        Meals[3].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[3] };
        Meals[4].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[4] };
        Meals[5].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[5] };
        Meals[6].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[6] };
        Meals[7].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[7] };
        Meals[8].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[8] };
        Meals[9].Feedbacks = new List<FeedbackTestEntity> { Feedbacks[9] };

        Orders[0].OrderItems = new List<OrderItemTestEntity> { OrderItems[0], OrderItems[1] };
        Orders[1].OrderItems = new List<OrderItemTestEntity> { OrderItems[2], OrderItems[3], OrderItems[4] };
        Orders[2].OrderItems = new List<OrderItemTestEntity> { OrderItems[5], OrderItems[6] };
        Orders[3].OrderItems = new List<OrderItemTestEntity> { OrderItems[7] };
        Orders[4].OrderItems = new List<OrderItemTestEntity> { OrderItems[8] };
        Orders[5].OrderItems = new List<OrderItemTestEntity> { OrderItems[9] };
        Orders[6].OrderItems = new List<OrderItemTestEntity> { OrderItems[10] };
        Orders[6].OrderItems = new List<OrderItemTestEntity> { OrderItems[11] };
        Orders[6].OrderItems = new List<OrderItemTestEntity> { OrderItems[12], OrderItems[13] };
        Orders[6].OrderItems = new List<OrderItemTestEntity> { OrderItems[14] };
    }

    public Mock<IEFCoreUnitOfWork> UnitOfWorkMock { get; set; }
}