using System.Security.Claims;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Commands.MealCommands;

public record CreateMealCommand(MealCreateModel MealCreateModel, ClaimsPrincipal User) : IRequest<MealDetailModel>;
