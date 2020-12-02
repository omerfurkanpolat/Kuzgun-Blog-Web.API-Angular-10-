using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfMessageRepository : EfRepositoryBase<Message, SahikaContext>, IMessageRepository
    {
        private SahikaContext _context;
        public EfMessageRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }
    }
}