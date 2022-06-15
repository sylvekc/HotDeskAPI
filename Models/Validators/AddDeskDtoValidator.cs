using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotDeskAPI.Entities;

namespace HotDeskAPI.Models.Validators
{
    public class AddDeskDtoValidator : AbstractValidator<AddDeskDto>
    {
        public AddDeskDtoValidator(HotDeskDbContext dbContext)
        {
            RuleFor(x => x.Available).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DeskNumber).Custom((value, context) =>
            {
                var deskInUse = dbContext.Desks.Any(u => u.DeskNumber == value);
                if (deskInUse)
                {
                    context.AddFailure("DeskNumber", $"Desk with number {value} currenty exist. Add desk with unique number.");
                }
            });


            RuleFor(x => x.LocationName).NotEmpty().Custom((value, context) =>
            {
                var locationExist = dbContext.Locations.Any(u => u.Name == value);
                if (!locationExist)
                {
                    context.AddFailure("LocationName", $"Location {value} doesn't exist.");
                }
            });
        }
    }
}
