using ServiceAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Dtos
{
    public class CarOutputDto
    {
        public string MarkGeo { get; set; }
        public string MarkEng { get; set; }
        public string ModelGeo { get; set; }
        public string ModelEng { get; set; }
        public string VinCode { get; set; }
        public string TransportNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public Owner Owner { get; set; }
        public Color Color { get; set; }
        public Fuel Fuel { get; set; }
        public int ActiveOwnerId { get; set; }
    }
}
