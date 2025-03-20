using System;
using System.Threading.Tasks;

namespace TestGame
{
    public abstract class BaseSystemInitializer<T> : ISystemInitializer
    {
        public Type Type => typeof(T);

        public abstract Task<BaseSystem> InitAsync();
    }
}