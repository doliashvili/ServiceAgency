using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Queries
{
    public class GetCars
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }


        public GetCars()
        {

        }

        [JsonConstructor]
        public GetCars(int? page,
                       int? pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
