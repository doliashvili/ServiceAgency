using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceAgency.Application.Dtos
{
    public class CarInputDto
    {
        [Required]
        [MaxLength(100)]
        public string MarkGeo { get; set; }
        [Required]
        [MaxLength(100)]
        public string MarkEng { get; set; }
        [Required]
        [MaxLength(100)]
        public string ModelGeo { get; set; }
        [Required]
        [MaxLength(100)]
        public string ModelEng { get; set; }
        [Required]
        [MaxLength(100)]
        public string VinCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string TransportNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string CreatedDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Image { get; set; }
        [Required]
        public List<int> OwnersId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Color { get; set; }
        [Required]
        [MaxLength(100)]
        public string Fuel { get; set; }
        [Required]
        public int ActiveOwnerId { get; set; }
    }
}
