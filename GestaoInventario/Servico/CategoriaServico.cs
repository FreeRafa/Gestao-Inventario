using GestaoInventario.Infraestrutura.Repositorio;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Task<Categoria> CriarCategoriaAsync(Categoria categoria)
        {
            return _categoriaRepositorio.CriarCategoriaAsync(categoria);
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
