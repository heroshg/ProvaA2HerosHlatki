using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProvaA2HerosHlatkiAPI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        [MaxLength(80, ErrorMessage = "O máximo de caracteres deve ser 80")]
        [MinLength(2, ErrorMessage = "Deve se ter no mínimo 2 caracteres no nome!")]
        [Required]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Campo CPF deve ser preenchido")]
        [StringLength(14, ErrorMessage = "O campo deve conter exatamente 14 caracteres")]
        [MaxLength(14, ErrorMessage = "O campo deve conter no máximo14 caracteres")]
        [MinLength(11, ErrorMessage = "O campo deve conter exatamente 14 caracteres")]
        public string? CPF { get; set; }
        [JsonIgnore]
        public List<Folha>? Folhas { get; set; }
    }
}
