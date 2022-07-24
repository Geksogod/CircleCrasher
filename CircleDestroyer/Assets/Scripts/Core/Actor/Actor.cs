using UnityEngine;

namespace Core.ActorModel
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private ActorComponent[] _actorComponents;
    }
}