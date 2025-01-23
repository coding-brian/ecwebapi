﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Dto.Product
{
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public Guid? SkuId { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsInBanner { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsInHomepage { get; set; }

        public bool IsInMainSection { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        public IList<ProductImageDto> Images { get; set; }
    }
}