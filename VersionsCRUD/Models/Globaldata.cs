using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Globaldata
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public int? Idlanguage { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }

        internal IEnumerable<object> GetGlobalDataByTypeAndLanguage(string v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
