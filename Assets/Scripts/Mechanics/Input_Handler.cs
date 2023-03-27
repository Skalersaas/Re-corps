using UnityEngine;

namespace GamePlay
{
    public class Input_Handler : MonoBehaviour
    {
        [SerializeField] Player pp;
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            bool Sneak = Input.GetKey(KeyCode.LeftControl);
            bool Roll = Input.GetKeyDown(KeyCode.Space);
            bool Punch = Input.GetKey(KeyCode.Mouse1);
            Vector2 dir = new Vector2(x, y);
            Vector2 c_Dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pp.In(dir, c_Dir, Sneak, Roll);
        }
    }
}