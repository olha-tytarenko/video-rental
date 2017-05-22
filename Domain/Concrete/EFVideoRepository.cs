using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFVideoRepository : Abstract.IVideoRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Video> video
        {
            get { return context.video; }
        }
    }
}
