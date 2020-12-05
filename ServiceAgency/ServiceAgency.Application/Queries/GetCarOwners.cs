using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Queries
{
    public class GetCarOwners
    {
        public int CarId { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public GetCarOwners()
        {

        }

        [JsonConstructor]
        public GetCarOwners(int carId, int? page, int? pageSize)
        {
            CarId = carId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
