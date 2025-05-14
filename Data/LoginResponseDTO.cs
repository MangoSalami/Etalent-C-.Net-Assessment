namespace Etalent_C__.Net_Assessment.Data
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
    }
}
