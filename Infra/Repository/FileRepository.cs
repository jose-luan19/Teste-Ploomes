using Infra.DBConfiguration.EFCore;
using File = Models.File;

namespace Infra.Repository
{
    public class FileRepository : ARepository<File>
    {
        public FileRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
