using Core;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        //private IProductRepository _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        //public IProductRepository productRepository =>
        //    _productRepository = _productRepository ?? new ProductRepository(_context);

        


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
