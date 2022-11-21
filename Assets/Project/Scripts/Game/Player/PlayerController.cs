using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    [RequireComponent(typeof(DOTweenPath))]
    public class PlayerController : PlayerShooter, IPlayerController
    {
        [Header("Components")]
        [SerializeField] private DOTweenPath tweenPath;
        [SerializeField] private Transform path;

#if UNITY_EDITOR
        public Vector3[] Waypoints => this.tweenPath.wps.ToArray();
#endif
        private void OnValidate()
        {
            if (this.tweenPath == null)
                this.tweenPath = base.GetComponent<DOTweenPath>();

            var pathWayPoints = new List<Vector3>();// { base.transform.position };
            int totalWayPoints = this.path.childCount;

            for (int index = 0; index < totalWayPoints; index++)
            {
                Transform pathSegment = this.path.GetChild(index);
                pathWayPoints.Add(pathSegment.position);
            }
            this.tweenPath.wps = pathWayPoints;
        }

        private void Awake()
        {
            Destroy(this.path.gameObject);
        }

        public void ToggleMovement(bool enabled)
        {
            if (enabled)
                this.tweenPath.DOPlay();
            else
                this.tweenPath.DOPause();
        }
    }
}