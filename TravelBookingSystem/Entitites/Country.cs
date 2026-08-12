using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class Country:AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
