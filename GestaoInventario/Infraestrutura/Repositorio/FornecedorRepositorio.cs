using GestaoInventario.Infraestrutura.Data;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoInventario.Infraestrutura.Repositorio
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        private readonly GestaoInventarioContext _context;

        public FornecedorRepositorio(GestaoInventarioContext context)
        {
            _context = context;
        }

        public async Task<Fornecedor?> ObterPorIdAsync(int id)
        {
            return await _context.Fornecedor.FindAsync(id);
        }

        public async Task<Fornecedor> CriarFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor> AtualizarFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Fornecedor.Update(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor?> DeletarFornecedorAsync(int id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedor.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
            return fornecedor;
        }

        public async Task<List<Fornecedor>> ObterTodosFornecedoresAsync()
        {
            return await _context.Fornecedor.ToListAsync();
        }

        public async Task<Fornecedor?> ObterPorNifAsync(string Nif)
        {
            return await _context.Fornecedor
                .FirstOrDefaultAsync(f => f.Nif == Nif);
        }
    }
}
