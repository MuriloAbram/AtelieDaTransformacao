using System;
using System.Collections.Generic;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para o cabeçalho do Pedido.
    /// Consolida os valores totais, status e vínculos com cliente e funcionário.
    /// </summary>
    public class OrderDto
    {
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public int IdEmployee { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalValue { get; set; }
        public string Status { get; set; } = string.Empty;

        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>(); 
    }
}