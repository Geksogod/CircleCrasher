using System;
using Core.CameraModule;
using UnityEngine;
using UnityEngine.Events;
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
            if (Input.GetMouseButtonDown(0))
            {
                SendEvent(OnBeginDrag, Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                SendEvent(OnDrag, Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SendEvent(OnEndDrag, Input.mousePosition);
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
                        SendEvent(OnEndDrag, _lastScreenPos.Value);
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
                    SendEvent(OnBeginDrag, firstTouch.position);
                }
                else if (_lastTouchPhase != TouchPhase.Ended)
                {
                    if (_curFingerId == -1) return;
                    SendEvent(OnDrag, firstTouch.position);
                    _lastScreenPos = firstTouch.position;
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

        private void SendEvent(Action<Vector3> dragEvent,Vector3 position)
        {
            var pos = _camera.Camera.ScreenToWorldPoint(position);
            dragEvent.Invoke(pos);
        }
    }
}