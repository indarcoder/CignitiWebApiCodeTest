using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestCode.Data;

namespace XUnitTestProject
{
    public class ConnectionFactory : IDisposable
    {
        
        private bool disposedValue = false; 
        public DataContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "mytestdatabase").Options;

            var context = new DataContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        } 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }       
    }
}
