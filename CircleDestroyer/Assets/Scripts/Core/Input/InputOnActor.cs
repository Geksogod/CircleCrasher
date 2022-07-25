using System;
using Core.ActorModel;
using Core.CameraModule;
using UnityEngine;

namespace Core.InputModule
{
    public class InputOnActor
    {
        public InputOnActor(InputProcess inputProcess)
        {
            inputProcess.OnBeginDrag += ActorTouchHandler;
        }

        private void ActorTouchHandler(Vector3 touchPos)
        {
            var touchObject = Physics2D.OverlapPoint(touchPos);
            if (touchObject == null)
            {
                return;
            }
            var actorTouchObject = touchObject.GetComponentInParent<IActorTouch>();
            actorTouchObject?.OnTouch();
        }
    }
}