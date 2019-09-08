using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public CustomerFormViewModel()
        {
            Id = 0;
        }
        public CustomerFormViewModel(Customers customers)
        {
            Id = customers.Id;
            Name = customers.Name;
            DateOfBirth = customers.DateOfBirth;
            MembershipTypeId = customers.MembershipTypeId;
        }
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Customer" : "New Customer";
            }
        }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18IfAMember]
        public DateTime? DateOfBirth { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        
        [Display(Name = "Membership Type")]
        [Required]
        public byte? MembershipTypeId { get; set; }
    }
}