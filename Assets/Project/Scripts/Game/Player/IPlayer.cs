using UnityEngine;

namespace game
{
    public interface IPlayer
    {
        public void OnGetAttacked(int damage);
        public void OnPathBlocked();
        public void OnPathUnblocked();
        public Vector3 GetPosition();
    }
}
