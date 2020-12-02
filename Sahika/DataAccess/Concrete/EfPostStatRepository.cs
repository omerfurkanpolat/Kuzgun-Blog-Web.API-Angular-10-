using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfPostStatRepository : EfRepositoryBase<PostStat, SahikaContext>, IPostStatRepository
    {
        private SahikaContext _context;
        public EfPostStatRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

    }
}