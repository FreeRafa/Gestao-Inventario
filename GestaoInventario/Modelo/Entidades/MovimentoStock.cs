using System;
using System.Collections.Generic;
using System.Text;
using GestaoInventario.Modelo.Enums;

namespace GestaoInventario.Modelo.Entidades
{
    public class MovimentoStock
    {
        public int Id { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
        public string? Observacao { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }

}
