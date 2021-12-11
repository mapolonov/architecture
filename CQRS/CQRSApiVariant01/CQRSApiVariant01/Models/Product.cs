using System;
using System.ComponentModel.DataAnnotations;
using CQRSApiVariant01.Infrastructure.MsSql;

namespace CQRSApiVariant01.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ProductType Type { get; set; }

        public DateTime Created { get; set; }
    }
}
