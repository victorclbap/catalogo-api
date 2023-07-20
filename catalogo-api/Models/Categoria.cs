using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace catalogo_api.Models
{
    [Table("Categorias")]
    public class Categoria
    {

        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        //quando possui id no nome o EF entende que é uma chave primária quando cria as tabelas
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        //propiedade de navegação
        public ICollection<Produto>? Produtos { get; set; }
    }
}
