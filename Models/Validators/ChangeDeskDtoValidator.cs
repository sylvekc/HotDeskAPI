using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotDeskAPI.Entities;

namespace HotDeskAPI.Models.Validators
{
    public class ChangeDeskDtoValidator : AbstractValidator<ChangeDeskDto>
    {
        public ChangeDeskDtoValidator(HotDeskDbContext dbContext)
        {
            RuleFor(x => x.DeskNumber).Custom((value, context) =>
            {
                var existingDesk = dbContext.Desks.Any(x => x.DeskNumber == value);
                if (!existingDesk)
                {
                    context.AddFailure($"Desk with number {value} doesn't exist.");
                }
            });

            RuleFor(x => x.DeskLocation).Custom((value, context) =>
            {
                var existingLocation = dbContext.Locations.Any(x => x.Name == value);
                if (!existingLocation)
                {
                    context.AddFailure($"Location {value} doesn't exist.");
                }
            });
        }
    }
}
