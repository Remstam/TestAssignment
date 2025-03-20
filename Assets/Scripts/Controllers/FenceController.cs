using UnityEngine;

namespace TestGame
{
    public class FenceController : MonoBehaviour
    {
        [SerializeField] private Transform[] _fenceSides;

        public Transform[] FenceSides => _fenceSides;
    }
}