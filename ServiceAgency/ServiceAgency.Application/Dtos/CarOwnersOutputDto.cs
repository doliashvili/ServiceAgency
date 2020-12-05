using ServiceAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Dtos
{
    public class CarOwnersOutputDto 
    {
        public List<OwnerOutPutDto> Owners { get; set; }
        public int ActiveOwnerId { get; set; }
    }
}
