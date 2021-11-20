using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateASP.Data.EntityClasses
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
