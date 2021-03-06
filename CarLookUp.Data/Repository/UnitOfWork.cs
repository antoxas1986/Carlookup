﻿using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Repository.Interfaces;
using System;

namespace CarLookUp.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICarContext _db;
        private bool _disposed = false;

        public UnitOfWork(ICarContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Saves the changes in Entity Framework.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
