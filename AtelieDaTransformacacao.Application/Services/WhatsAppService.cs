using System.Web;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

/// <summary>
/// Service responsible for formatting the message with product data and generating the direct link to the seller's WhatsApp.
/// </summary>
public class WhatsAppService : IWhatsAppService
{
    // Número de telefone do Ateliê (com o código do país 55 e o DDD)
    private readonly string _merchantPhoneNumber = "5511999999999";

    /// <summary>
    /// Cria uma URL dinâmica estruturada para iniciar uma conversa no WhatsApp já preenchida com o nome e valor do produto.
    /// </summary>
    /// <param name="productName">Nome do produto de interesse do cliente.</param>
    /// <param name="price">Preço atual do produto.</param>
    /// <returns>Uma string contendo a URL completa de redirecionamento (Ex: https://wa.me/...)</returns>
    public string GenerateProductInquiryLink(string productName, decimal price)
    {
        // Monta a mensagem que o cliente enviará ao clicar (usando asteriscos para negrito no WhatsApp)
        string message = $"Olá! Fiquei muito interessado no produto *{productName}* no valor de {price:C}. Gostaria de combinar o pagamento e a entrega!";

        // Codifica a mensagem para que espaços e caracteres especiais funcionem perfeitamente dentro de uma URL HTTP
        string encodedMessage = HttpUtility.UrlEncode(message);

        // Retorna o link final estruturado na API oficial do WhatsApp
        return $"https://wa.me/{_merchantPhoneNumber}?text={encodedMessage}";
    }
}