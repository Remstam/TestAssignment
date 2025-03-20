using System;
using UnityEngine;

namespace TestGame
{
    public interface IBattleMember
    {
        event Action<IBattleMember, IBattleMember, float> DamageSent;
        event Action<IBattleMember> HasDied;

        string BattleName { get; }
        Vector3 Position { get; }
        
        bool IsAlive();
        void TakeDamage(string from, float damage);
    }
}