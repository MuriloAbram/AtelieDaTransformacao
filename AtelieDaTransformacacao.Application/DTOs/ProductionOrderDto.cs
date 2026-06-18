using System;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para a Ordem de Produção (OP).
    /// Controla o fluxo de trabalho de fabricação na oficina dos artesãos.
    /// </summary>
    public class ProductionOrderDto 
    {
        public int IdOp { get; set; }
        public int IdEmployee { get; set; }
        public int IdProduct { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
    }
}