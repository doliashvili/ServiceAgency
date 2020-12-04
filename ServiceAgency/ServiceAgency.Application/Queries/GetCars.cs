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
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }


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
