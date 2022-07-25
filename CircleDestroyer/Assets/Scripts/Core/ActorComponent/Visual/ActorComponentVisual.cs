using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    public class ActorComponentVisual : ActorComponent
    {
        [SerializeField] private SpriteRenderer _renderer;
        [Inject] private ActorContainer actorContainer;

        public override void Configurate(Actor actor)
        {
            base.Configurate(actor);
            _renderer.enabled = true;
            _renderer.sprite = actorContainer.GetActorSprite(ComponentActor.ActorType);
        }
    }
}