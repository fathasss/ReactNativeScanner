namespace JwtAuthForBooks.Models;

public class GenerateTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime TokenExpireDate { get; set; } = DateTime.Now;
}