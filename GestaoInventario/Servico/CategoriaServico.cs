using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;

namespace GestaoInventario.Servico
{
    public class CategoriaServico
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public Task<List<Categoria>> ObterTodasCategoriasAsync()
        {
            return _categoriaRepositorio.ObterTodasCategoriasAsync();
        }

        public Task<Categoria?> ObterPorIdAsync(int id)
        {
            return _categoriaRepositorio.ObterPorIdAsync(id);
        }

        public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
        {
            var categoriaExistente = await _categoriaRepositorio.ObterPorNomeAsync(categoria.Nome);

            if (categoriaExistente != null)
            {
                throw new InvalidOperationException($"Já existe uma categoria com o nome '{categoria.Nome}'.");
            }

            return await _categoriaRepositorio.CriarCategoriaAsync(categoria);
        }

        public Task<Categoria> AtualizarCategoriaAsync(Categoria categoria)
        {
            return _categoriaRepositorio.AtualizarCategoriaAsync(categoria);
        }

        public Task<Categoria?> DeletarCategoriaAsync(int id)
        {
            return _categoriaRepositorio.DeletarCategoriaAsync(id);
        }
    }
}
