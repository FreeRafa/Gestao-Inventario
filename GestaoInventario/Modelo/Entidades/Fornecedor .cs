using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Modelo.Entidades
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nif { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public ICollection<MovimentoStock> Movimentos { get; set; } = new List<MovimentoStock>();
    }
}
