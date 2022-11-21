using TMPro;
using UnityEngine;

namespace game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreDisplay : MonoBehaviour
    {
        private const string KILL_COUNT_DISPLAY = "Score: {0}";

        [SerializeField] private TextMeshProUGUI scoreText;

        private int killCount;

        private void OnValidate()
        {
            if (this.scoreText == null)
                this.scoreText = base.GetComponent<TextMeshProUGUI>();
        }

        public void OnKill()
        {
            this.scoreText.text = string.Format(KILL_COUNT_DISPLAY, ++this.killCount);
        }
    }
}