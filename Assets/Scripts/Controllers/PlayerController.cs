using System;
using UnityEngine;

namespace TestGame
{
     [RequireComponent(typeof(Health))]
     public class PlayerController : MonoBehaviour, IBattleMemberComponent
     {
          public event Action OnPlayerDie = delegate { };
          public event Action<IBattleMemberComponent, bool> OnCollided = delegate { };

          public IBattleMember BattleMember { get; private set; }
          public Vector3 Position => transform.position;
          public Quaternion Rotation => transform.rotation;
          public Vector3 WeaponPosition => _weapon.SpawnPoint.position;
          public Quaternion WeaponRotation => _weapon.SpawnPoint.rotation;
          public Health Health => _health ?? GetComponent<Health>();

          [SerializeField] private PlayerWeapon _weapon;

          private PlayerConfig _config;
          private MoveBorders _moveBorders;
          private Health _health;
          private Rigidbody _rb;

          public void Init(IBattleMember battleMember, PlayerConfig config, MoveBorders moveBorders)
          {
               BattleMember = battleMember;
               
               _config = config;
               _moveBorders = moveBorders;
               
               // TODO: seems awkward, would you like to go back to physics fence collisions?
               _moveBorders.left += transform.localScale.x / 2f;
               _moveBorders.right -= transform.localScale.x / 2f;
               _moveBorders.up -= transform.localScale.z / 2f;
               _moveBorders.down += transform.localScale.z / 2f;

               _weapon.OnCollision += OnCollision;
               
               Health.Init(config.Health);
               Health.OnDie += OnDie;
          }

          private void OnDie()
          {
               OnPlayerDie?.Invoke();
               Health.OnDie -= OnDie;
          }

          public void TakeDamage(float damage)
          {
               Health.TakeDamage(damage);
          }
          
          public void Move(Vector3 move)
          {
               transform.position += move.normalized * (_config.MoveSpeed * Time.fixedDeltaTime);

               var pos = transform.position;
               pos.x = Mathf.Clamp(pos.x, _moveBorders.left, _moveBorders.right);
               pos.z = Mathf.Clamp(pos.z, _moveBorders.down, _moveBorders.up);
               transform.position = pos;

               var rot = Quaternion.LookRotation(move);
               transform.rotation = Quaternion.Slerp(transform.rotation, rot, _config.RotationSpeed * Time.fixedDeltaTime);
          }

          private void OnCollision(Collision collision, bool isActive)
          {
               var battleMember = collision.gameObject.GetComponent<IBattleMemberComponent>();
               if (battleMember != null)
               {
                    OnCollided?.Invoke(battleMember, isActive);
               }
          }

          private void OnDestroy()
          {
               if (Health != null)
               {
                    Health.OnDie -= OnDie;
               }

               if (_weapon != null)
               {
                    _weapon.OnCollision -= OnCollision;
               }
          }
     }
}