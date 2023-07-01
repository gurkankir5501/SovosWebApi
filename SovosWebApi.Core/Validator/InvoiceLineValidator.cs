using FluentValidation;
using FluentValidation.Results;
using SovosWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.Validator
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLine>
    {
        public override ValidationResult Validate(ValidationContext<InvoiceLine> context)
        {
            RuleFor(s => s.Name)
                .MaximumLength(30)
                .WithErrorCode("Name field cannot exceed 200 characters ! ");
            RuleFor(s => s.UnitCode)
                .MaximumLength(30)
                .WithErrorCode("Unit code field cannot exceed 200 characters ! ");
          
            return base.Validate(context);
        }
    }
}
