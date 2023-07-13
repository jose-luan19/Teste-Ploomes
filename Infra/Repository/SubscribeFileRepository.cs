using Infra.DBConfiguration.EFCore;
using Models;

namespace Infra.Repository
{
    public class SubscribeFileRepository : ARepository<SubscribeFile>
    {
        public SubscribeFileRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
