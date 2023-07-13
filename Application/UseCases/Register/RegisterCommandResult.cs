namespace Application.UseCases.Register
{
    public class RegisterCommandResult
    {
        public bool hasLogin { get; set; }
        public string? Message { get; set; }
    }
}