using GestaoInventario.Infraestrutura.Data;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoInventario.Infraestrutura.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly GestaoInventarioContext _context;

        public CategoriaRepositorio(GestaoInventarioContext context)
        {
            _context = context;
        }

        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            return await _context.Categoria.FindAsync(id);
        }

        public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> AtualizarCategoriaAsync(Categoria categoria)
        {
            _context.Categoria.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria?> DeletarCategoriaAsync(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            return categoria;
        }

        public async Task<List<Categoria>> ObterTodasCategoriasAsync()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria?> ObterPorNomeAsync(string nome)
        {
            return await _context.Categoria
                .FirstOrDefaultAsync(c => c.Nome == nome);
        }
    }
}
