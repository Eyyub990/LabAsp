using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    public class SubscribeRepository : AsyncRepository<Subscribe>, ISubscriberRepository
    {
        public SubscribeRepository(DbContext db)
            :base(db)
        {
            
        }
    }
}
