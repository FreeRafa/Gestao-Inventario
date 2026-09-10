using GestaoInventario.Infraestrutura.Data;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace GestaoInventario.Infraestrutura.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly GestaoInventarioContext _context;

        public ProdutoRepositorio(GestaoInventarioContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> AtualizarProdutoAsync(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto?> DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return produto;
        }

        public async Task<List<Produto>> ObterTodosProdutosAsync()
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorCodigoAsync(string codigo)
        {
            return await _context.Produto
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
        }
    }
}
