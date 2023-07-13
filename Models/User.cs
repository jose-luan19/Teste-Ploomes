using Models;

namespace reality_subscribe_api.Model
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
