using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller responsável pela gestão de autenticação de utilizadores (Login, Registo e Logout).
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
    /// Processa a tentativa de login do utilizador.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // Proteção: Se a rota anterior gravada na memória tentar empurrar o utilizador para o /Admin,
            // e ele for um utilizador comum, ignoramos o redirecionamento antigo e mandamos para a Vitrina (Home)
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) && !returnUrl.Contains("/Admin"))
            {
                return Redirect(returnUrl);
            }

            // Caso contrário, vai direto para a página pública
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Dados de acesso inválidos.");
        return View(model);
    }

    /// <summary>
    /// Exibe a página de registo de novos perfis.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        // CORREÇÃO: Passa um modelo vazio para evitar o erro de NullReferenceException na View
        return View(new RegisterDto());
    }

    /// <summary>
    /// Regista um novo utilizador no sistema utilizando Identity e redireciona para a Vitrina.
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
            // Efetua o login automático logo após a criação da conta
            await _signInManager.SignInAsync(user, isPersistent: false);

            // CORREÇÃO CRUCIAL: Redireciona para a vitrina pública ("Home"), impedindo o erro de Access Denied
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        // Devolve o modelo para não quebrar a View se houver erros de validação
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