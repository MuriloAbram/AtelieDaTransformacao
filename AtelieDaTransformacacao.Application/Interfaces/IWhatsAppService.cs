using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Interface encarregada de estruturar a mensagem customizada e gerar o link do WhatsApp.
/// </summary>
public interface IWhatsAppService
{
    string GenerateProductInquiryLink(string productName, decimal price);
}