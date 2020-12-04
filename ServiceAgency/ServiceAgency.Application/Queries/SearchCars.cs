using Newtonsoft.Json;
using System;

namespace ServiceAgency.Application.Queries
{
    public class SearchCars
    {
        public string MarkGeo { get; private set; }
        public string MarkEng { get; private set; }
        public string ModelGeo { get; private set; }
        public string ModelEng { get; private set; }
        public string VinCode { get; private set; }
        public string TransportNumber { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public string Color { get; private set; }
        public string Fuel { get; private set; }
        public int? OwnerId { get; private set; }
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }
        public bool OrderingDescending { get; private set; }
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
