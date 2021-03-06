﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEngine.Models
{
    public class Restaurante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo nome não pode ser vazio")]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        public List<Prato> pratos { get; set; }
    }
}
