using FluentValidation;

namespace O2morny.Application.Features.Shop
{
    public class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
    {
        public CreateShopCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.CityId)
                .GreaterThan(0);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180);

            RuleFor(x => x.WorkingHours)
                .Must(HaveValidWorkingHours)
                .WithMessage("Working hours contain invalid or overlapping periods.");

            RuleFor(x => x.Images)
                .Must(HaveAtMostOneMainImage)
                .WithMessage("Only one main image is allowed.");
        }

        private static bool HaveAtMostOneMainImage(List<ShopImageDto> images)
        {
            return images.Count(x => x.IsMain) <= 1;
        }

        private static bool HaveValidWorkingHours(List<ShopWorkingHourDto> hours)
        {
            foreach (var hour in hours)
            {
                if (hour.OpenAt >= hour.CloseAt)
                    return false;
            }

            var groups = hours
                .GroupBy(x => x.DayOfWeek);

            foreach (var group in groups)
            {
                var sorted = group
                    .OrderBy(x => x.OpenAt)
                    .ToList();

                for (int i = 1; i < sorted.Count; i++)
                {
                    if (sorted[i].OpenAt < sorted[i - 1].CloseAt)
                        return false;
                }
            }

            return true;
        }
    }
}
