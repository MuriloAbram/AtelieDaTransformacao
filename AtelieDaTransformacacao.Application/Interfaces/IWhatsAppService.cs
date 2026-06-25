using System;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface encarregada de estruturar a mensagem customizada e gerar o link do WhatsApp.
    /// </summary>
    public interface IWhatsAppService
    {
        /// <summary>
        /// Cria uma URL dinâmica estruturada para iniciar uma conversa no WhatsApp já preenchida com o nome e valor do produto.
        /// </summary>
        /// <param name="productName">Nome do produto de interesse do cliente.</param>
        /// <param name="price">Preço atual do produto.</param>
        /// <returns>Uma string contendo a URL completa de redirecionamento (Ex: https://wa.me/...)</returns>
        string GenerateProductInquiryLink(string productName, decimal price);
    }
}