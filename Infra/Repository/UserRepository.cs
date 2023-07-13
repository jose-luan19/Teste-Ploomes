using Infra.DBConfiguration.EFCore;
using reality_subscribe_api.Model;

namespace Infra.Repository
{
    public class UserRepository : ARepository<User>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
