using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Models
{
    public class Prato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo restaurante não pode ser vazio")]
        public int RestauranteId { get; set; }
        [Required(ErrorMessage = "Campo nome não pode ser vazio")]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo preço não pode ser vazio")]
        public decimal Preco { get; set; }

        public Restaurante restaurante { get; set; }
    }
}
