using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Commands.FeedbackCommands;

public record CreateMealFeedbackCommand(MealFeedbackCreateModel MealFeedbackCreateModel) : IRequest<FeedbackDetailModel>;
