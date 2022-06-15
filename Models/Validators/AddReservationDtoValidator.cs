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
            RuleFor(x => x.To).Custom((value, context) =>
            {
                var day = DateTime.Now.AddDays(1);
                var week = DateTime.Now.AddDays(7);
                if (value < day)
                {
                    context.AddFailure("You have to reserve desk at least for 1 day.");
                }

                if (value > week)
                {
                    context.AddFailure("You can reserve desk max for week");
                }
            });
        }
    }
}
