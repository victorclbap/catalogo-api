using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace catalogo_api.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e bo mínimo {2} caracteres ")]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string? Descricao { get; set; }

        // decimal possui precisão maior
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")] // define a coluna como tendo 8 dígitos com 2 casas decimais
        [Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        //chave estrangeira e propriedade de navegação
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
