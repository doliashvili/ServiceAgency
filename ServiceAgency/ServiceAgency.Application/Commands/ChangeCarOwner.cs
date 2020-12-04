using Newtonsoft.Json;
using ServiceAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Commands
{
    public class ChangeCarOwner
    {
        [JsonConstructor]
        public ChangeCarOwner(int ownerId, int carId)
        {
            OwnerId = ownerId;
            CarId = carId;
        }
        public ChangeCarOwner()
        {

        }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int OwnerId { get; set; }
    }
}
