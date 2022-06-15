using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotDeskAPI.Entities;
using Microsoft.OpenApi.Writers;

namespace HotDeskAPI.Models.Validators
{
    public class AddReservationDtoValidator : AbstractValidator<AddReservationDto>
    {
        public AddReservationDtoValidator(HotDeskDbContext dbContext)
        {
            RuleFor(x => x.DeskNumber).Custom((value, context) =>
            {
                var desk = dbContext.Desks.Any(x => x.DeskNumber == value);
                if (!desk)
                {
                    context.AddFailure($"Desk with number: {value} doesn't exist.");
                }
            });

            RuleFor(x => x.LocationName).Custom((value, context) =>
            {
                var location = dbContext.Locations.Any(x => x.Name == value);
                if (!location)
                {
                    context.AddFailure($"Location {value} doesn't exist");
                }
            });

            RuleFor(x => x.From).Must(x => x.Date > DateTime.Now);

        }
    }
}
