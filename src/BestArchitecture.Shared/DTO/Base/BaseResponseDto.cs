namespace BestArchitecture.Shared.DTO.Base
{
    public class BaseResponseDto
    {
        public bool? Success { get; set; }
        public List<string> Errors { get; set; } = [];
        public string? Message { get; set; }
    }
}
