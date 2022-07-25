using System.Linq;

namespace Core.ActorModel
{
    public class ActorStopByTouch : ActorComponent,IActorComponentTouchHandler
    {
        public void OnActorTouch()
        {
            var actorMoveByTouch = 
                (ActorMoveByTouch)ComponentActor.ActorComponents.
                    FirstOrDefault(a => a.GetType() == typeof(ActorMoveByTouch));

            if (actorMoveByTouch == null)
            {
                return;
            }
            actorMoveByTouch.Stop();
        }
    }
}