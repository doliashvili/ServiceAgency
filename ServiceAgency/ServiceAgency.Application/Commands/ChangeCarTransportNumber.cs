using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Commands
{
    public class ChangeCarTransportNumber
    {
        [JsonConstructor]
        public ChangeCarTransportNumber(string transportNumber,int carId)
        {
            TransportNumber = transportNumber;
            CarId = carId;
        }
        public ChangeCarTransportNumber()
        {

        }
        [Required]
        [MaxLength(100)]
        public string TransportNumber { get; set; }
        [Required]
        public int CarId { get; set; }
    }
}
