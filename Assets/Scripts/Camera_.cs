using UnityEngine;

public class Camera_ : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float speed;
    Vector3 pos;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        pos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + Player.position) / 2;
        pos = (Player.position + 3 * pos) / 4;
        pos -= transform.position;
        pos.z = 0f;
        transform.Translate(pos * speed * Time.deltaTime);
    }
}