using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SpatialDotNet
{
    // Async property wrapper
    public sealed class AsyncProperty<T>
    {
        private readonly Func<Task<T>> _getter;

        public AsyncProperty(Func<T> factory)
        {
            _getter = () => Task.Run(factory);
        }

        public AsyncProperty(Func<Task<T>> factory)
        {
            _getter = () => Task.Run(factory);
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return _getter().GetAwaiter();
        }

        public void Start()
        {
            _getter().Start();
        }

        public static implicit operator AsyncProperty<T>(T obj)
        {
            return new AsyncProperty<T>(() => obj);
        }

        public static implicit operator T(AsyncProperty<T> obj)
        {
            return obj._getter().Result;
        }
    }
}