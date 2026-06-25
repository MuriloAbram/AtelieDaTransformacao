using AtelieDaTransformacao.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedirectController : Controller
    {
        public RedirectController()
        {
        }

        /// <summary>
        /// Redireciona o usuário para uma URL externa informada via query string.
        /// </summary>
        /// <param name="url">URL completa de destino (ex: https://google.com)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RedirectToUrl([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest(new { message = "A URL de destino não pode estar vazia." });
            }

            // Valida se a string enviada é uma URL válida e absoluta
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                return BadRequest(new { message = "A URL fornecida é inválida. Certifique-se de incluir http:// ou https://" });
            }

            // Retorna o HTTP Status 302 para o navegador mudar de página
            return Redirect(url);
        }
    }
}