using trade_compas.DTOs.Auth;
using trade_compas.Interfaces;

namespace trade_compas.Repositories;

public class AuthRepository(Supabase.Client supabaseClient) : IAuthRepository
{
    public async Task Login(LoginDto dto)
    {
        await supabaseClient.Auth.SignIn(dto.Email, dto.Password);
    }

    public async Task SignUp(SignupDto dto)
    {
        await supabaseClient.Auth.SignUp(dto.Email, dto.Password);
    }

    public async Task Logout()
    {
        await supabaseClient.Auth.SignOut();
    }
}