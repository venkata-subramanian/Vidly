using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipTypeDTO MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
    }
}