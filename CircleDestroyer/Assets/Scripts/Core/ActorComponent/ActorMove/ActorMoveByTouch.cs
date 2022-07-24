using Core.InputModule;
using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    public class ActorMoveByTouch : ActorComponent
    {
        [Inject] private InputProcess _inputProcess;

        public override void Configurate(Actor actor)
        {
            base.Configurate(actor);
            _inputProcess.OnDrag += OnDrag;
        }

        private void OnDrag(Vector3 vector3)
        {
            ComponentActor.transform.position = (Vector2)vector3;
        }
    }
}