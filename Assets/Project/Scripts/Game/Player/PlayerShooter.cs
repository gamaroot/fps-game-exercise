using UnityEngine;

namespace game
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private Ray shotRay;

        private void OnValidate()
        {
            if (this.audioSource == null)
                this.audioSource = base.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.audioSource.Play();

                this.shotRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(this.shotRay, out RaycastHit hit, float.MaxValue))
                {
                    Debug.Log(hit.collider.name);

                    Enemy enemy = hit.collider.GetComponent<Enemy>();

                    if (hit.collider.tag == GameTags.ZOMBIE)
                    {
                        hit.collider.GetComponent<Enemy>().Death();
                    }
                }
            }
        }
    }
}