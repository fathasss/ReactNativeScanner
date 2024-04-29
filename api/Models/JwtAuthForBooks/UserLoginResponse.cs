namespace JwtAuthForBooks.Models;

public class UserLoginResponse
{
    public bool AuthenticateResult { get; set; } = false;
    public string AuthToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpireDate { get; set; } = DateTime.Now;
}