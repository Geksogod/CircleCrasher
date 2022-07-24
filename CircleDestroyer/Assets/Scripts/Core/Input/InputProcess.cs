using System;
using Core.CameraModule;
using UnityEngine;
using Zenject;
using Input = UnityEngine.Input;

namespace Core.InputModule
{
    public class InputProcess : MonoBehaviour
    {
        [Inject] private GameCameraModule _camera;
        public Action<Vector3> OnDrag; 
        public Action<Vector3> OnBeginDrag; 
        public Action<Vector3> OnEndDrag; 
        
        public void Update()
        {
#if UNITY_EDITOR
            ProcessEditorInput();
#else
            ProcessTouchInput();
#endif
        }

        private void ProcessEditorInput()
        {
            var screenPos = _camera.Camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.touchCount < 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnBeginDrag?.Invoke(screenPos);
                }
                else if (Input.GetMouseButton(0))
                {
                    OnDrag?.Invoke(screenPos);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnEndDrag?.Invoke(screenPos);
            }
        }
    }
}