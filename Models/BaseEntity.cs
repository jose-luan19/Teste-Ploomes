namespace Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifieddDate { get; set; } = DateTime.Now;


    }
}
