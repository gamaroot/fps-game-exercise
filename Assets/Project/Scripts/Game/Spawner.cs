using System;
using UnityEngine;

namespace game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Spawnable[] spawnables;

        private IPlayer player;
        private Action onEnemyDeath;

        public void Setup(IPlayer player, Action onEnemyDeath)
        {
            this.player = player;
            this.onEnemyDeath = onEnemyDeath;
        }

        private void OnTriggerEnter(Collider other)
        {
            Vector3 spawnPos = base.transform.position;
            spawnPos.y += 1f;

            Spawnable spawnPrefab = this.spawnables[UnityEngine.Random.Range(0, this.spawnables.Length)];
            Spawnable spawn = Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
            spawn.transform.SetParent(base.transform.parent, true);

            spawn.GetComponent<Enemy>()?.Setup(this.player, this.onEnemyDeath);

            Destroy(base.gameObject);
        }
    }
}