using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller responsável pela gestão de autenticação de utilizadores (Login, Registo e Logout) do painel administrativo.
/// </summary>
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Exibe a página de login do sistema.
    /// </summary>
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Processa a tentativa de login do administrador.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Admin");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    /// <summary>
    /// Exibe a página de registo de novos administradores.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Regista um novo utilizador administrador no sistema utilizando Identity.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Admin");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }

    /// <summary>
    /// Efetua o logout do utilizador e redireciona para a página pública.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Exibe a página de acesso negado quando um utilizador tenta aceder a uma área sem permissões.
    /// </summary>
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}