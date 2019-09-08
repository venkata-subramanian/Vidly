using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18IfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customers) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.UnknownMembershipType || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.DateOfBirth == null)
                return new ValidationResult("Date of Birth is required");

            return (DateTime.Now.Year - customer.DateOfBirth.Value.Year > 18) ? ValidationResult.Success : new ValidationResult("Customer has to be more than 18 years for the selected subscription");
        }
    }
}