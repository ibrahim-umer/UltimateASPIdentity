using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.DatabaseContext;
using UltimateASP.Data.EntityClasses;
using UltimateASP.RepoOps.IRepo;

namespace UltimateASP.RepoOps.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly HotelListingDBContext _context;
        private IGenericRepo<Country> _countries;
        private IGenericRepo<Hotel> _hotels;
        public UnitOfWork(HotelListingDBContext context)
        {
            _context = context;
        }
        public IGenericRepo<Country> Countries => _countries ??= new GenericRepo<Country>(_context);
        public IGenericRepo<Hotel> Hotels => _hotels ??= new GenericRepo<Hotel>(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
            
            GC.SuppressFinalize(this);
        }

    }
}
