using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para os Itens do Pedido.
    /// Representa de forma isolada a quantidade e subtotal de cada produto vendido.
    /// </summary>
    public class OrderItemDto 
    {
        public int IdItem { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}