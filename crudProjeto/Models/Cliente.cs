using System.ComponentModel.DataAnnotations;

namespace crudProjeto.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 8)]
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
    }
}