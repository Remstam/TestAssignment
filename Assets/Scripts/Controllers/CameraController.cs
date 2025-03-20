using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<Camera> _cameras;
        private Transform _player;
        private Transform _followCamera;
        private int _currentCameraIndex;
        private Vector3 _stance;

        public void Init(Transform player)
        {
            _player = player;
            _followCamera = _cameras[1].transform;
            _stance = _followCamera.position - player.position;
        }

        public void SwitchCamera()
        {
            _currentCameraIndex++;
            _currentCameraIndex = _currentCameraIndex >= _cameras.Count ? 0 : _currentCameraIndex;
                    
            ToggleCamera(_cameras, _currentCameraIndex);
        }

        public void UpdateFollowCamera()
        {
            _followCamera.position = _player.position + _stance;
        }
        
        private void ToggleCamera(List<Camera> cameras, int cameraIndex)
        {
            foreach (var cam in cameras)
            {
                cam.enabled = false;
            }

            cameras[cameraIndex].enabled = true;
        }
    }
}