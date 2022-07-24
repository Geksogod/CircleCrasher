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

        private int _curFingerId;
        private TouchPhase _lastTouchPhase;
        private Vector3? _lastScreenPos = null;

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
            if (Input.GetMouseButtonDown(0))
            {
                OnBeginDrag?.Invoke(screenPos);
            }
            else if (Input.GetMouseButton(0))
            {
                OnDrag?.Invoke(screenPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnEndDrag?.Invoke(screenPos);
            }
        }

        private void ProcessTouchInput()
        {
            if (Input.touchCount > 0)
            {
                var firstTouch = Input.GetTouch(0);

                if (_curFingerId != -1 && _curFingerId != firstTouch.fingerId) return;

                _lastTouchPhase = firstTouch.phase;
                if (_lastTouchPhase is TouchPhase.Ended or TouchPhase.Canceled)
                {
                    _curFingerId = -1;
                    if (_lastScreenPos != null)
                    {
                        var pos = _camera.Camera.ScreenToWorldPoint(_lastScreenPos.Value);
                        OnEndDrag?.Invoke(pos);
                    }

                    _lastScreenPos = null;
                }

                if (Input.touchCount > 1)
                {
                    return;
                }

                if (_lastTouchPhase == TouchPhase.Began)
                {
                    _curFingerId = firstTouch.fingerId;

                    var pos = _camera.Camera.ScreenToWorldPoint(firstTouch.position);
                    OnBeginDrag?.Invoke(pos);
                }
                else if (_lastTouchPhase != TouchPhase.Ended)
                {
                    if (_curFingerId == -1) return;
                    
                    var pos = _camera.Camera.ScreenToWorldPoint(firstTouch.position);
                    OnDrag?.Invoke(pos);
                }
            }
            else
            {
                if (_lastTouchPhase == TouchPhase.Ended || _lastScreenPos == null) return;
                _curFingerId = -1;
                OnEndDrag.Invoke(_lastScreenPos.Value);
                _lastScreenPos = null;
            }
        }
    }
}