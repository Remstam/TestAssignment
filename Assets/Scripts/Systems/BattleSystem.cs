using System.Collections.Generic;

namespace TestGame
{
    public class BattleSystem : BaseSystem, IDebugEnemiesSystem
    {
        public HashSet<IBattleMember> BattleMembers => _battleMembers;
        
        private readonly IBattleMember _battlePlayer;
        private readonly EnemiesSystem _enemiesSystem;
        private readonly HashSet<IBattleMember> _battleMembers = new();
        
        public BattleSystem(IBattleMember battlePlayer, EnemiesSystem enemiesSystem)
        {
            _battlePlayer = battlePlayer;
            _enemiesSystem = enemiesSystem;
        }

        public void AddBattleMember(IBattleMember battleMember)
        {
            _battleMembers.Add(battleMember);
        }

        public void RemoveBattleMember(IBattleMember battleMember)
        {
            _battleMembers.Remove(battleMember);
        }
        
        public void Init()
        {
            _battleMembers.Add(_battlePlayer);
            _enemiesSystem.EnemySpawned += OnEnemySpawned;

            foreach (var battleMember in _battleMembers)
            {
                battleMember.HasDied += OnBattleMemberDied;
                battleMember.DamageSent += OnDamageSent;
            }
            
            _enemiesSystem.Init();
        }

        private void OnDamageSent(IBattleMember from, IBattleMember to, float damage)
        {
            if (_battleMembers.Contains(to))
            {
                to.TakeDamage(from.BattleName, damage);
            }
        }
        
        private void OnEnemySpawned(IBattleMember battleMember)
        {
            _battleMembers.Add(battleMember);
            battleMember.HasDied += OnBattleMemberDied;
            battleMember.DamageSent += OnDamageSent;
        }

        private void OnBattleMemberDied(IBattleMember battleMember)
        {
            _battleMembers.Remove(battleMember);
            battleMember.HasDied -= OnBattleMemberDied;
            battleMember.DamageSent -= OnDamageSent;
        }
        
        public override void Dispose()
        {
            _enemiesSystem.EnemySpawned -= OnEnemySpawned;
            foreach (var battleMember in _battleMembers)
            {
                if (battleMember == null)
                {
                    continue;
                }
                
                battleMember.HasDied -= OnBattleMemberDied;
                battleMember.DamageSent -= OnDamageSent;
            }
            
            _battleMembers.Clear();
        }

        public void GiveRandomDamage(float damage)
        {
            if (_battleMembers.Count == 0)
            {
                return;
            }

            var battleMembersCopy = new HashSet<IBattleMember>(_battleMembers);
            battleMembersCopy.Remove(_battlePlayer);

            if (battleMembersCopy.Count == 0)
            {
                return;
            }

            var randomEnemy = battleMembersCopy.GetRandom();
            randomEnemy?.TakeDamage("DEBUG", damage);
        }
    }
}