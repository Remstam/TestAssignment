using System;
using UnityEngine;

namespace TestGame
{
    public interface ITarget
    {
        // TODO: possible configurable target priority
        public int Priority { get; }
        Vector3 Position { get; }
    }

    public interface IChaser
    {
        event Action<IChaser> StoppedChase;
        
        Vector3 Position { get; }
        void SetTarget(ITarget target);
    }
}