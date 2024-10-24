using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sigmatech.Interfaces.Entities
{
    public interface IBaseEntity
    {
        public Guid id { get; set; }

        DateTime? createdAt { get; set; }
        string? createdBy { get; set; }

        DateTime? updatedAt { get; set; }
        string? updatedBy { get; set; }
        
        DateTime? deletedAt { get; set; }
        string? deletedBy { get; set; }
    }
}