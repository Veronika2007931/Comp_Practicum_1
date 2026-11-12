using CoffeeShopApi.DTOs;
using FluentValidation;

namespace CoffeeShopApi.Validators;

public class CreateUpdateMenuItemValidator : AbstractValidator<CreateUpdateMenuItemDto>
{
    public CreateUpdateMenuItemValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Назва позиції меню є обов'язковою.")
        .Length(2, 100).WithMessage("Назва повинна містити від 2 до 100 символів.");

        RuleFor(x => x.Description)
        .MaximumLength(500).WithMessage("Опис позиції не може перевищувати 500 символів.");

        RuleFor(x => x.Price)
        .GreaterThan(0).WithMessage("Ціна повинна бути більшою за 0 грн.")
        .LessThanOrEqualTo(1000).WithMessage("Ціна не може перевищувати 10 000 грн.");

        RuleFor(x => x.VolumeMl)
        .InclusiveBetween(10, 2000).WithMessage("Об'єм або вага повинні бути в межах від 10 до 2000 (мл/г).");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Необхідно вказати коректний ідентифікатор категорії (CategoryId > 0).");

    }
}