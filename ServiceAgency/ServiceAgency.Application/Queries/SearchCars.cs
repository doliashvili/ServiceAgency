using Newtonsoft.Json;
using System;

namespace ServiceAgency.Application.Queries
{
    public class SearchCars
    {
        public string MarkGeo { get; set; }
        public string MarkEng { get; set; }
        public string ModelGeo { get; set; }
        public string ModelEng { get; set; }
        public string VinCode { get; set; }
        public string TransportNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Color { get; set; }
        public string Fuel { get; set; }
        public int? OwnerId { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public bool OrderingDescending { get; set; }
        public SearchCars()
        {

        }

        [JsonConstructor]
        public SearchCars(string markGeo,
                          string markEng,
                          string modelGeo,
                          string modelEng,
                          string vinCode,
                          string transportNumber,
                          DateTime? createdDate,
                          string color,
                          string fuel,
                          int? ownerId,
                          int? page,
                          int? pageSize,
                          bool orderingDescending)
        {
            MarkGeo = markGeo;
            MarkEng = markEng;
            ModelGeo = modelGeo;
            ModelEng = modelEng;
            VinCode = vinCode;
            TransportNumber = transportNumber;
            CreatedDate = createdDate;
            Color = color;
            Fuel = fuel;
            OwnerId = ownerId;
            Page = page;
            PageSize = pageSize;
            OrderingDescending = orderingDescending;
        }
    }
}
