using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sigmatech.Interfaces.Entities;

namespace Sigmatech.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public string? createdBy { get; set; }

        public DateTime? createdAt { get; set; }

        public string? updatedBy { get; set; }

        public DateTime? updatedAt { get; set; }

        public string? deletedBy { get; set; }

        public DateTime? deletedAt { get; set; }
    }

    
}