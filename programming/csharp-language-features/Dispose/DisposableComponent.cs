using System;
using System.IO;

namespace Edward.Wilde.CSharp.Features.Dispose
{
    public class DisposableComponent : IDisposable
    {
        private FileSystemWatcher watcher;

        public DisposableComponent()
        {
            this.watcher = new FileSystemWatcher();            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here
                    if (watcher != null)
                    {
                        watcher.Dispose();
                        watcher = null;
                    }
                }

                // Dispose unmanaged resources here

                disposed = true;
            }
        }

        ~DisposableComponent()
        {
            Dispose(false);
        }

        private bool disposed;
    }

    public class DisposeExample
    {
        public void Run()
        {
            using (var component = new DisposableComponent())
            {
                // do work...
            }
        }
    }
}