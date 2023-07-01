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
    public class InvoiceHeaderValidator : AbstractValidator<InvoiceHeader>
    {
        public override ValidationResult Validate(ValidationContext<InvoiceHeader> context)
        {
            RuleFor(s => s.ReceiverTitle)
                .MaximumLength(200)
                .WithErrorCode("Receiver Title field cannot exceed 200 characters ! ");
            RuleFor(s => s.SenderTitle)
                .MaximumLength(200)
                .WithErrorCode("Sender Title field cannot exceed 200 characters ! ");
            RuleFor(s => s.InvoiceId)
                .MaximumLength(15)
                .WithErrorCode("InvoiceId field cannot exceed 200 characters ! ");

            RuleForEach(x => x.Invoices).SetValidator(new InvoiceLineValidator());

            return base.Validate(context);
        }
    }
}
