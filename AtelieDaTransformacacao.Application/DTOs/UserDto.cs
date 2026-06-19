using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de transferência de dados para o usuário/vendedor.
    /// </summary>
    public class UserDto
    {
        public string Id { get; set; } = string.Empty; // Identificador único do usuário
        public string FullName { get; set; } = string.Empty; // Nome completo do usuário
        public string Email { get; set; } = string.Empty; // Endereço de e-mail do usuário
        public string WhatsAppNumber { get; set; } = string.Empty; // Número de WhatsApp do usuário
    }
}