using System;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para o fluxo financeiro de Pagamentos.
    /// </summary>
    public class PaymentDto
    {
        public int IdPayment { get; set; }
        public int IdOrder { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
}