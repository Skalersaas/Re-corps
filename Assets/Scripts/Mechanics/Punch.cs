using UnityEngine;

namespace GamePlay
{
    public class Punch : MonoBehaviour
    {
        [SerializeField] float lifetime;
        float t;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                print('5');
                t = 0;
            }
        }
        private void OnEnable()
        {
            t = lifetime;
        }
        private void Update()
        {
            t -= Time.deltaTime;
            if (t <= 0)
                gameObject.SetActive(false);
        }
    }
}