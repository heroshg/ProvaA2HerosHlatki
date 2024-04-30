using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProvaA2HerosHlatkiAPI.Models
{
    public class Folha
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        [Range(1, 12, ErrorMessage = "O mês deve estar entre Janeiro 1 e Dezembro 12")]
        public int Mes { get; set; }
        [Range(0, 2024, ErrorMessage = "O ano deve ser de 0 até 2024 que é o ano atual.")]
        public int Ano { get; set; }
        public double SalarioBruto { get; set; }
        public double ImpostoIrrf { get; set; }
        public double ImpostoInss { get; set; }
        public double ImpostoFgts { get; set; }
        public double SalarioLiquido { get; set; }
        
        public Funcionario? Funcionario { get; set; }
        public int FuncionarioId { get; set; }

        public Folha( double valor, int quantidade, int mes, int ano, int funcionarioId)
        {
            Valor = valor;
            Quantidade = quantidade;
            Mes = mes;
            Ano = ano;
            SalarioBruto = Valor * Quantidade;
            if (SalarioBruto <= 1903.98)
            {
                ImpostoIrrf = 0;
            }
            else if (SalarioBruto > 1903.99 && SalarioBruto <= 2826.65)
            {
                ImpostoIrrf = 142.80;
            }
            else if (SalarioBruto > 2826.66 && SalarioBruto <= 3751.05)
            {
                ImpostoIrrf = 354.80;
            }
            else if (SalarioBruto > 3751.05 && SalarioBruto <= 4664.68)
            {
                ImpostoIrrf = 636.13;
            }
            else if (SalarioBruto > 4664.68) ImpostoIrrf = 869.36;
            if (SalarioBruto <= 1693.72)
            {
                ImpostoInss = SalarioBruto * 0.8;
            }
            else if (SalarioBruto > 1693.72 && SalarioBruto <= 2822.90)
            {
                ImpostoInss = SalarioBruto * 0.9;
            }
            else if (SalarioBruto > 2822.90 && SalarioBruto <= 5645.80)
            {
                ImpostoInss = SalarioBruto * 0.11;
            }
            else if (SalarioBruto > 5645.80) ImpostoInss = 621.03;
            ImpostoFgts = SalarioBruto * 0.08;
            SalarioLiquido = SalarioBruto - ImpostoIrrf - ImpostoInss;
            FuncionarioId = funcionarioId;
        }

        

    }
}
