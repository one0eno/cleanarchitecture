using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Video: BaseDomainModel
    {

        public string Name { get; set; }

        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public virtual Director Director { get; set; }
        public Video()
        {
            Actors = new HashSet<Actor>();                
        }
     
    }
}
