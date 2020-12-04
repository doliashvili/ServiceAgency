using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Commands
{
    public class ChangeCarColor
    {
        [JsonConstructor]
        public ChangeCarColor(string color, int carId)
        {
            Color = color;
            CarId = carId;
        }
        public ChangeCarColor()
        {

        }
        public string Color { get; set; }
        public int CarId { get; set; }
    }
}
