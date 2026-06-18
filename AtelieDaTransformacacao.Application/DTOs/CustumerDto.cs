using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para a entidade Cliente.
    /// Trafega dados cadastrais e de contato dos clientes do ateliê.
    /// </summary>
    public class CustomerDto
    {
        public int IdCustomer { get; set; } // Identificador do Cliente, chave primária
        public string Name { get; set; } = string.Empty; // Nome completo do cliente, utilizado para identificação e comunicação
        public string CpfCnpj { get; set; } = string.Empty; // CPF para pessoas físicas ou CNPJ para pessoas jurídicas, utilizado para identificação fiscal e registros de compras
        public string Phone { get; set; } = string.Empty; // Número de telefone para contato, pode incluir código de área, utilizado para comunicação e atendimento ao cliente
        public string Email { get; set; } = string.Empty; // Endereço de e-mail para contato, utilizado para comunicação digital, envio de confirmações e marketing
        public string Address { get; set; } = string.Empty; // Endereço 
    }
}