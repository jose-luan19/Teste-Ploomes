using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Attachment { get; set; }
        public string Type { get; set; }

    }
}
