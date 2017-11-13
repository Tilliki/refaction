using System.Collections.Concurrent;
using System.Threading;

namespace refactor_me.Database
{
    /// <summary>
    ///     A pool of database context connections.
    /// </summary>
    public class ProductDataPool
    {
        private readonly ConcurrentStack<ProductDataContext> _pool;
        private readonly SemaphoreSlim _semaphore;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductDataPool" />.
        /// </summary>
        /// <param name="poolSize">The number of connections in the data pool.</param>
        public ProductDataPool(int poolSize)
        {
            _pool = new ConcurrentStack<ProductDataContext>(new ProductDataContext[poolSize]);
            _semaphore = new SemaphoreSlim(poolSize);
        }

        /// <summary>
        ///     Check out a single connection. If none are available, and there is space left, create one.
        /// </summary>
        /// <returns>The data context.</returns>
        public ProductDataContext CheckOut()
        {
            _semaphore.Wait();
            ProductDataContext productData;
            _pool.TryPop(out productData);
            return productData ?? new ProductDataContext();
        }

        /// <summary>
        ///     Return the data context to the pool.
        /// </summary>
        /// <param name="productData">The context to check in.</param>
        public void CheckIn(ProductDataContext productData)
        {
            _pool.Push(productData);
            _semaphore.Release();
        }
    }
}