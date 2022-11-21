using UnityEngine;

namespace game
{
    public interface IPlayer
    {
        public void OnGetAttacked(float damage);
        public void OnPathBlocked();
        public void OnPathUnblocked();
        public Vector3 GetPosition();
    }
}
