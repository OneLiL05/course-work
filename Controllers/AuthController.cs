using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using Supabase.Gotrue.Exceptions;
using trade_compas.DTOs.Auth;
using trade_compas.Interfaces;
using Client = Supabase.Client;

namespace trade_compas.Controllers;

public class AuthController(Client supabaseClient, IAuthRepository authRepository) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    [HttpGet("login")]
    public IActionResult Login()
    {
        if (_user != null) return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await supabaseClient.Auth.SignIn(dto.Email, dto.Password);

                return RedirectToAction("Index", "Products");
            }
            catch (GotrueException ex)
            {
                var json = JsonSerializer.Deserialize<IDictionary<string, string>>(ex.Message);

                ViewData["Error"] = json["error_description"]!;
            }
        }

        return View(dto);
    }

    [HttpGet("signup")]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await supabaseClient.Auth.SignUp(dto.Email, dto.Password);

                return RedirectToAction("Login");
            }
            catch (GotrueException ex)
            {
                var json = JsonSerializer.Deserialize<IDictionary<string, string>>(ex.Message);

                ViewData["Error"] = json["error_description"];
            }
        }

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        try
        {
            await supabaseClient.Auth.SignOut();

            Console.WriteLine(_user.Id);

            return RedirectToAction("Index", "Products");
        }
        catch (GotrueException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}