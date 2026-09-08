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
    public class MovimentoStockRepositorio : IMovimentoStockRepositorio
    {
        private readonly GestaoInventarioContext _context;

        public MovimentoStockRepositorio(GestaoInventarioContext context)
        {
            _context = context;
        }

        public async Task<MovimentoStock?> ObterPorIdAsync(int id)
        {
            return await _context.MovimentoStock
                .Include(m => m.Produto)
                .Include(m => m.Fornecedor)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<MovimentoStock> CriarMovimentoStockAsync(MovimentoStock movimentoStock)
        {
            _context.MovimentoStock.Add(movimentoStock);
            await _context.SaveChangesAsync();
            return movimentoStock;
        }

        public async Task<MovimentoStock> AtualizarMovimentoStockAsync(MovimentoStock movimentoStock)
        {
            _context.MovimentoStock.Update(movimentoStock);
            await _context.SaveChangesAsync();
            return movimentoStock;
        }

        public async Task<MovimentoStock?> DeletarMovimentoStockAsync(int id)
        {
            var movimentoStock = await _context.MovimentoStock.FindAsync(id);
            if (movimentoStock != null)
            {
                _context.MovimentoStock.Remove(movimentoStock);
                await _context.SaveChangesAsync();
            }
            return movimentoStock;
        }

        public async Task<List<MovimentoStock>> ObterMovimentosPorProdutoIdAsync(int produtoId)
        {
            return await _context.MovimentoStock
                .Include(m => m.Produto)
                .Include(m => m.Fornecedor)
                .Where(m => m.ProdutoId == produtoId)
                .ToListAsync();
        }


    }
}
