using UnityEngine;

namespace Core.CameraModule
{
    public class GameCameraModule : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public Camera Camera => _camera;
    }
}