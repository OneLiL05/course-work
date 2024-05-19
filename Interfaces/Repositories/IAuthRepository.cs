using trade_compas.DTOs.Auth;

namespace trade_compas.Interfaces;

public interface IAuthRepository
{
    Task Login(LoginDto dto);

    Task SignUp(SignupDto dto);

    Task Logout();
}