using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImageHost.Models.Data
{
    public class DbWrapper : IDisposable
    {
        private readonly DbContext _context;
        private bool _disposed;

        public GenericRepository<HostedImage> Images { get; set; }

        public DbWrapper(DbContext context)
        {
            _context = context;

            Images = new GenericRepository<HostedImage>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}