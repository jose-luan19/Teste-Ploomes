using reality_subscribe_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SubscribeFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Subscribe Subscribe { get; set; }
        public Guid SubscribeId { get; set; }
        public File File { get; set; }
        public Guid FileId { get; set;}
    }
}
