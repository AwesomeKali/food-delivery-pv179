using AutoMapper;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;

public class UpdateOrderAddressCommandHandler : CommandHandler<UpdateOrderAddressCommand, AddressDetailModel>, IRequestHandler<UpdateOrderAddressCommand, AddressDetailModel>
{
    public UpdateOrderAddressCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<AddressDetailModel> Handle(UpdateOrderAddressCommand request, CancellationToken cancellationToken)
    {
        var addressEntity = _mapper.Map<AddressEntity>(request.AddressUpdateModel);

        var unitOfWork = _unitOfWorkProvider.Create();
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.orderId);
        addressEntity.Id = (int) order.AddressId;
        unitOfWork.AddressRepository.Update(addressEntity);
        await unitOfWork.Commit();

        return _mapper.Map<AddressDetailModel>(addressEntity);
    }
}
