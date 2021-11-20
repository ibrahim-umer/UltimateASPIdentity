using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;
using UltimateASP.Models;

namespace UltimateASP.MappingConfig
{
    public class MapperInit: Profile
    {
        public MapperInit()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
