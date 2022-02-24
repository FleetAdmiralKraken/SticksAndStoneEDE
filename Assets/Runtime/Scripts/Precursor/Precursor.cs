using UnityEngine;

namespace com.cringejam.sticksandstones
{
    public class Precursor : MonoBehaviour
    {
        public enum State
        {
            Default,
            Combat
        }

        [Header("Dependencies")]
        [SerializeField] private GameObject defaultGraphics = default;
        [SerializeField] private GameObject combatGraphics = default;
        [SerializeField] private Enemy enemy = default;

        private State state = default;

        private void Start()
        {
            enemy.OnDamageDealtEvent += OnDamageDealtDelegate;
        }
        private void OnDestroy()
        {
            enemy.OnDamageDealtEvent -= OnDamageDealtDelegate;
        }

        public void SetState(State state)
        {
            if (this.state == state)
                return;

            this.state = state;
            UpdateGraphics(state);
        }

        private void OnDamageDealtDelegate(object sender, int damage)
        {
            SetState(State.Combat);
        }

        private void UpdateGraphics(State state)
        {
            switch (state)
            {
                case State.Default:
                    {
                        defaultGraphics.SetActive(true);
                        combatGraphics.SetActive(false);

                        break;
                    }
                case State.Combat:
                    {
                        defaultGraphics.SetActive(false);
                        combatGraphics.SetActive(true);

                        break;
                    }
            }
        }
    }
}
