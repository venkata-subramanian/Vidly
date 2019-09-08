using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Customer
            Mapper.CreateMap<CustomerDTO, Customers>().ForMember(c => c.Id,opt => opt.Ignore());
            Mapper.CreateMap<Customers, CustomerDTO>();
            Mapper.CreateMap<MembershipType, MembershipTypeDTO>();

            // Movie
            Mapper.CreateMap<MovieDTO, Movies>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Movies, MovieDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();
        }
    }
}