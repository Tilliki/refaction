using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace refactor_me.Database
{
    public class ProductDataPool
    {
        private SemaphoreSlim _semaphore;
        private ConcurrentStack<ProductDataContext> _pool;

        public ProductDataPool(int poolSize)
        {
            _pool = new ConcurrentStack<ProductDataContext>(new ProductDataContext[poolSize]);
            _semaphore = new SemaphoreSlim(poolSize);
        }

        public ProductDataContext CheckOut()
        {
            _semaphore.Wait();
            ProductDataContext productData;
            _pool.TryPop(out productData);
            return productData ?? new ProductDataContext();
        }

        public void CheckIn(ProductDataContext productData)
        {
            _pool.Push(productData);
            _semaphore.Release();
        }
    }
}