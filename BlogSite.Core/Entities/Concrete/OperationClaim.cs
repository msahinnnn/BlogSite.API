using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Entities.Concrete
{
    public class OperationClaim : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
