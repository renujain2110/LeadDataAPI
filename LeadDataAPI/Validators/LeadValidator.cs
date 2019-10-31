using FluentValidation;
using LeadDataAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadDataAPI.Validators
{
    /// <summary>
    /// Validator class for Lead Entity
    /// </summary>
    public class LeadValidator : AbstractValidator<Lead>
    {
        
        public LeadValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.AcceptTerms).NotNull();
        }
    }
}
