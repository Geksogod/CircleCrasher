using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.ActorModel
{
    [CreateAssetMenu(fileName = "ActorContainer", menuName = "ActorContainer", order = 0)]
    public class ActorContainer : ScriptableObject
    {
        //can move to serialize dictionary
        [SerializeField]private ActorContainerItem[] _actorContainerItems;

        [CanBeNull]
        public Actor GetActorByType(ActorType actorType)
        {
            return _actorContainerItems.FirstOrDefault(a => a.ActorType == actorType)?.Actor;
        }
    }

    [Serializable]
    public class ActorContainerItem
    {
        [SerializeField] private ActorType _actorType;
        [SerializeField] private Actor _actor;

        public ActorType ActorType => _actorType;
        public Actor Actor => _actor;
    }
}