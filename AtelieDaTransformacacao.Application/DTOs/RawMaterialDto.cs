using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para as Matérias-Primas.
    /// Alinha o consumo físico e estocagem de insumos como madeira bruta e resinas.
    /// </summary>
    public class RawMaterialDto
    {
        public int IdMaterial { get; set; } // Identificador único da matéria-prima
        public string MaterialName { get; set; } = string.Empty; // Nome da matéria-prima, utilizado para identificação e controle de estoque
        public int IdSupplier { get; set; } // Identificador do fornecedor, chave estrangeira para vincular a matéria-prima ao fornecedor responsável pelo fornecimento
        public int StockQuantity { get; set; } // Quantidade atual em estoque, utilizada para controle de inventário e planejamento de compras
        public string MeasurementUnit { get; set; } = string.Empty; // Unidade de medida para a matéria-prima, como "kg", "litros" ou "metros", utilizada para padronizar a quantificação e facilitar o controle de estoque
        public decimal Measurement { get; set; } // Quantidade medida da matéria-prima, utilizada para controle de estoque e planejamento de produção, pode representar o peso, volume ou comprimento dependendo da unidade de medida
    }
}
