using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Feature
{
    public class FeatureGetByIdResp
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Release { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }

    }
}
