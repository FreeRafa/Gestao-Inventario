using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Modelo.Interfaces
{
    public interface ICategoriaRepositorio
    {
        public Task<Categoria?>ObterPorIdAsync(int id);
        public Task<Categoria>CriarCategoriaAsync(Categoria categoria);
        public Task<Categoria> AtualizarCategoriaAsync(Categoria categoria);
        public Task<Categoria?> DeletarCategoriaAsync(int id);
        public Task<List<Categoria>> ObterTodasCategoriasAsync();
    }
}
