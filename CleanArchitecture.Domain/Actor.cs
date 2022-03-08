using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Actor: BaseDomainModel
    {

        public string? Name { get; set; }

        public string? SurName { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public Actor()
        {
            Videos  = new HashSet<Video>();
        }
       
    }
}
