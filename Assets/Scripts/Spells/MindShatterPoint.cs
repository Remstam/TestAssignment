using UnityEngine;

namespace TestGame
{
    public class MindShatterPoint : ITarget
    {
        public int Priority => 2;
        public Vector3 Position { get; set; }
    }
}