using System;
using UnityEngine;

namespace Core.ActorModel
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private ActorComponent[] _actorComponents;

        private void Start()
        {
            foreach (var actorComponent in _actorComponents)
            {
                actorComponent.Configurate(this);
            }
        }
    }
}