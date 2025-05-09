namespace BestArchitecture.Application.DTO
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
