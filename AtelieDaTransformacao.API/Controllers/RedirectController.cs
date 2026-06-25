using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedirectController : Controller
{
    private readonly IWhatsAppService _whatsAppService;

    /// <summary>
    /// Construtor que injeta o serviço de WhatsApp mapeado no Program.cs
    /// </summary>
    public RedirectController(IWhatsAppService whatsAppService)
    {
        _whatsAppService = whatsAppService;
    }

    /// <summary>
    /// Endpoint que recebe os dados do produto e redireciona o cliente direto para o WhatsApp do Ateliê.
    /// </summary>
    /// <param name="productName">Nome do produto artesanal que o usuário tem interesse.</param>
    /// <param name="price">Preço do produto.</param>
    /// <returns>HTTP 302 (Redirect) para a URL oficial do WhatsApp com a mensagem montada.</returns>
    [HttpGet("whatsapp")]
    public IActionResult RedirectToWhatsApp([FromQuery] string productName, [FromQuery] decimal price)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            return BadRequest(new { message = "O nome do produto é obrigatório para gerar o redirecionamento." });
        }

        // Invoca a regra de negócio que você definiu na Service
        string whatsappUrl = _whatsAppService.GenerateProductInquiryLink(productName, price);

        // Executa o redirecionamento HTTP nativo
        return Redirect(whatsappUrl);
    }
}