using System.Linq;
using System.Web;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services
{
    /// <summary>
    /// Implementação do gerador de links dinâmicos baseados nas informações do produto desejado.
    /// </summary>
    public class WhatsAppService : IWhatsAppService
    {
        public string GenerateRedirectUrl(string phoneNumber, string productName, decimal price)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            var cleanNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            string rawMessage = $"Olá! Vi o seu trabalho no Ateliê da Transformação e fiquei interessado no produto \"{productName}\" (Valor: R$ {price:N2}). Ele ainda está disponível?";
            string encodedMessage = HttpUtility.UrlEncode(rawMessage);

            return $"https://wa.me/{cleanNumber}?text={encodedMessage}";
        }
    }
}