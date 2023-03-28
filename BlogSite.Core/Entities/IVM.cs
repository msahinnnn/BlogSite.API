using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Entities
{
    public interface IVM<T> where T : class, IBaseEntity, new()
    {
    }
}
