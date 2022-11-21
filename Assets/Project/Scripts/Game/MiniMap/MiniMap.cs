using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    [RequireComponent(typeof(RectTransform))]
    public class MiniMap : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RectTransform miniMap;
        [SerializeField] private Image playerIcon;
        [SerializeField] private Terrain terrain;

        private IPlayer player;

        private void OnValidate()
        {
            if (this.miniMap == null)
                this.miniMap = base.GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            this.playerIcon.rectTransform.anchoredPosition = this.ConvertWorldPositionToMiniMap(this.player.GetPosition());
        }

        public void Setup(IPlayer player)
        {
            this.player = player;
            this.playerIcon.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private Vector2 ConvertWorldPositionToMiniMap(Vector3 worldPosition)
        {
            Vector3 realMap3D = this.terrain.terrainData.size;
            var relativePosition = new Vector2(worldPosition.x / realMap3D.x, worldPosition.z / realMap3D.z);

            return new Vector2(this.miniMap.sizeDelta.x * relativePosition.x, this.miniMap.sizeDelta.y * relativePosition.y);
        }
    }
}