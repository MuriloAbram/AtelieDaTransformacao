using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para a entidade Funcionário.
    /// Contém informações sobre cargos, salários e identificação da equipe.
    /// </summary>
    public class EmployeeDto
    {
        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}