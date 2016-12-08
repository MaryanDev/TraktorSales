using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineSales.WebUI.Entities.DTOs
{
    public class FullMachineInfoDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public string Model { get; set; }
        public List<string> Images { get; set; }
    }
}