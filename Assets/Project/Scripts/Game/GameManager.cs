using UnityEngine;

namespace game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Player player;
        [SerializeField] Transform spawners;
        [SerializeField] ScoreDisplay scoreDisplay;
        [SerializeField] MiniMap miniMap;
        
        public ParticleSystem bloodEffect;
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;

            this.miniMap.Setup(this.player);

            Spawner[] spawners = this.spawners.GetComponentsInChildren<Spawner>();
            foreach (Spawner spawner in spawners)
                spawner.Setup(this.player, this.scoreDisplay.OnKill);
        }
    }
}