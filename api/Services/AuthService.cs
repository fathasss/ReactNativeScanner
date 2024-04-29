using api.Data;
using api.Helper;
using JwtAuthForBooks.Interfaces;
using JwtAuthForBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthForBooks.Services;

public class AuthService : IAuthService
{
    readonly ITokenService tokenService;

    private readonly ApplicationDBContext _context;

    public AuthService(ITokenService tokenService , ApplicationDBContext context)
    {
        this.tokenService = tokenService;
        this._context = context;
    }

    public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
    {
        UserLoginResponse response = new();

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        var employees = await _context.Employees.ToListAsync();
        var encryptedPassword = CryptographyHelper.Encrypt(request.Password);
        var employee = employees.FirstOrDefault(x => x.Email == request.Email && x.Password == encryptedPassword);

        if (employee != null)
        {
            var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest { Username = employee.Email });

            response.AuthenticateResult = true;
            response.AuthToken = generatedTokenInformation.Token;
            response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
        }

        return response;
    }
}