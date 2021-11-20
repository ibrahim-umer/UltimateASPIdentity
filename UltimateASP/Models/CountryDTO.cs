using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;

namespace UltimateASP.Models
{
    public class CountryDTO
    {
        public string Name { get; set; }
        public List<HotelDTO> Hotels { get; set; }
    }
}
