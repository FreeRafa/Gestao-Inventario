using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GestaoInventario.Infraestrutura.Data
{
    public class GestaoInventarioContext : DbContext
    {
        public GestaoInventarioContext(DbContextOptions<GestaoInventarioContext> options) : base(options)
        {
        }


    }
}
