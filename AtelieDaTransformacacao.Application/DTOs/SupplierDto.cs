namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para Fornecedores.
    /// </summary>
    public class SupplierDto
    {
        public int IdSupplier { get; set; } // Identificador único do fornecedor
        public string Name { get; set; } = string.Empty; // Nome do fornecedor, utilizado para identificação e comunicação
        public string Cnpj { get; set; } = string.Empty; // CNPJ do fornecedor
        public string Phone { get; set; } = string.Empty; // Telefone do fornecedor
        public string Email { get; set; } = string.Empty; // Email do fornecedor
    }
}