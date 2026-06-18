using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço responsável por gerar links dinâmicos do WhatsApp.
    /// </summary>
    public interface IWhatsAppService
    {
        string GenerateRedirectUrl(string phoneNumber, string productName, decimal price);
    }
}