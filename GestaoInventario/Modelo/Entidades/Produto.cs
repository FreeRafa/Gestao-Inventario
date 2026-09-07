using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Modelo.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public int QuantidadeEmStock { get; set; }
        public int StockMinimo { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public ICollection<MovimentoStock> Movimentos { get; set; } = new List<MovimentoStock>();

    }
}
