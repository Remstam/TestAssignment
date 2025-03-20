using System;
using System.Threading.Tasks;

namespace TestGame
{
    public interface ISystemInitializer
    {
        Type Type { get; }

        Task<BaseSystem> InitAsync();
    }
}