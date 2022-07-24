using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.ActorModel
{
    [CreateAssetMenu(fileName = "ActorContainer", menuName = "ActorContainer", order = 0)]
    public class ActorContainer : ScriptableObject
    {
        //can move to serialize dictionary
        [SerializeField]private ActorContainerItem[] _actorContainerItems;

        private readonly Dictionary<ActorType, ActorContainerItem> _usedActor = new();

        public Actor GetActorByType(ActorType actorType)
        {
            return GetActorItem(actorType).Actor;
        }

        public Sprite GetActorSprite(ActorType actorType)
        {
            return GetActorItem(actorType).Sprite;
        }

        private ActorContainerItem GetActorItem(ActorType actorType)
        {
            if (_usedActor.ContainsKey(actorType))
            {
                return _usedActor[actorType];
            }
            
            var actorContainerItem = _actorContainerItems.FirstOrDefault(a => a.ActorType == actorType);

            if (actorContainerItem == null)
            {
                throw new NullReferenceException($"Can't find actor by type - {actorType}");
            }
            _usedActor.Add(actorType,actorContainerItem);
            return actorContainerItem;
        }
    }

    [Serializable]
    public class ActorContainerItem
    {
        [SerializeField] private ActorType _actorType;
        [SerializeField] private Actor _actor;
        [SerializeField] private AssetReferenceSprite _assetReferenceSprite;

        public ActorType ActorType => _actorType;
        public Actor Actor => _actor;

        public Sprite Sprite => Addressables.LoadAssetAsync<Sprite>(_assetReferenceSprite).WaitForCompletion();
    }
}