using UnityEngine;
using UnityEngine.Events;

namespace com.cringejam.sticksandstones
{
    public class CollisionListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collision> onCollisionEnterEvent = default;
        [SerializeField] private UnityEvent<Collision> onCollisionExitEvent = default;

        private void OnCollisionEnter(Collision collision) => onCollisionEnterEvent.Invoke(collision);
        private void OnCollisionExit(Collision collision) => onCollisionExitEvent.Invoke(collision);
    }
}
