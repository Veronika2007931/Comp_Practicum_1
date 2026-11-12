using CoffeeShopApi.DTOs;
using FluentValidation;

namespace CoffeeShopApi.Validators;

public class CreateUpdateCategoryValidator : AbstractValidator<CreateUpdateCategoryDto>
{
    public CreateUpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва категорії є обов'язковою.")
            .Length(2, 50).WithMessage("Назва категорії повинна містити від 2 до 50 символів.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Опис категорії не може бути порожнім.")
            .MaximumLength(300).WithMessage("Опис категорії не може перевищувати 300 символів.");
    }
}