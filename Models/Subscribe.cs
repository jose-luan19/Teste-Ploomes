using Models;

namespace reality_subscribe_api.Model
{
    public class Subscribe : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<SubscribeFile> Files { get; set; }
        public bool Checked { get; set; } = false;
    }
}
