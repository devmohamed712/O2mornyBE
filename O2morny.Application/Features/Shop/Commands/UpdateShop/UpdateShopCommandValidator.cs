using FluentValidation;

namespace O2morny.Application.Features.Shop
{
    public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.OwnerId)
                .NotEmpty();

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
                .Must(HaveValidWorkingHours).WithMessage("Working hours contain invalid or overlapping periods.");

            RuleFor(x => x.Images)
                .Must(HaveAtMostOneMainImage).WithMessage("Only one main image is allowed.");

            RuleForEach(x => x.Images)
                .Must(HaveValidImageData).WithMessage("A new image must have an image file.");
        }

        private static bool HaveAtMostOneMainImage(List<ShopImageDto> images)
        {
            return images.Count(x =>
                !x.IsDeleted && x.IsMain) <= 1;
        }

        private static bool HaveValidImageData(ShopImageDto image)
        {
            // Existing image
            if (image.Id.HasValue)
                return true;

            // Deleted new image doesn't make sense
            if (image.IsDeleted)
                return false;

            // New image must have a file
            return image.ImageFile != null;
        }

        private static bool HaveValidWorkingHours(List<ShopWorkingHourDto> hours)
        {
            // Ignore deleted working hours
            var activeHours = hours
                .Where(x => !x.IsDeleted)
                .ToList();

            // OpenAt must be before CloseAt
            foreach (var hour in activeHours)
            {
                if (hour.OpenAt >= hour.CloseAt)
                    return false;
            }

            // Check overlapping periods per day
            foreach (var group in activeHours.GroupBy(x => x.DayOfWeek))
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
