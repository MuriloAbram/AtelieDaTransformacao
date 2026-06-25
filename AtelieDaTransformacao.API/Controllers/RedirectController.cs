using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedirectController : ControllerBase // 💡 AJUSTADO: Alterado para ControllerBase, ideal para APIs REST
    {
        private readonly IWhatsAppService _whatsAppService;

        // 💡 SEGURANÇA: Lista de domínios confiáveis do WhatsApp explicitamente autorizados (Allowlist)
        private static readonly string[] AllowedDomains = new[]
        {
            "wa.me",
            "api.whatsapp.com",
            "chat.whatsapp.com"
        };

        /// <summary>
        /// Construtor que injeta o serviço de WhatsApp mapeado no Program.cs
        /// </summary>
        public RedirectController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        /// <summary>
        /// Endpoint que recebe os dados do produto e redireciona o cliente direto para o WhatsApp do Ateliê com segurança.
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

            // Invoca a regra de negócio que você definiu na Service para construir o link
            string whatsappUrl = _whatsAppService.GenerateProductInquiryLink(productName, price);

            // =====================================================================
            // VALIDAÇÃO CONTRA OPEN REDIRECT (ALLOWLIST)
            // =====================================================================
            if (Uri.TryCreate(whatsappUrl, UriKind.Absolute, out var parsedUri))
            {
                var host = parsedUri.Host.ToLower();

                // Remove o prefixo 'www.' para uma validação limpa
                if (host.StartsWith("www."))
                    host = host.Substring(4);

                // Verifica se o domínio gerado pertence estritamente à API oficial da Meta/WhatsApp
                if (AllowedDomains.Any(domain => host == domain || host.EndsWith("." + domain)))
                {
                    // Se o domínio for seguro e confiável, executa o redirecionamento HTTP nativo
                    return Redirect(whatsappUrl);
                }
            }

            // Se por algum motivo o link gerado apontar para fora da Allowlist, bloqueia por segurança
            return BadRequest(new { message = "Falha de segurança: Tentativa de redirecionamento para um domínio não autorizado rejeitada." });
        }
    }
}