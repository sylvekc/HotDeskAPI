using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotDeskAPI.Entities;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace HotDeskAPI.Models.Validators
{
    public class AddLocationDtoValidator : AbstractValidator<AddLocationDto>
    {

        public AddLocationDtoValidator(HotDeskDbContext dbContext)
        {
            RuleFor(x => x.Floor).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RoomNumber).GreaterThan(0);
            RuleFor(x => x.Building).NotEmpty();
        }
    }
}
