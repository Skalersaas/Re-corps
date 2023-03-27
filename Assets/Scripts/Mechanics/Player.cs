using UnityEngine;

namespace GamePlay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed;
        float r_cool, w_cool, r_l;
        float ss;
        Rigidbody2D rb;
        bool Rolling, Sneaking, Wall;
        Vector2 R_pos;
        [SerializeField] Transform body, head;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void In(Vector2 Dir, Vector2 camera, params bool[] abil)
        {
            Sneak(abil[0]);
            Roll(abil[1], Dir);
            Move(Dir);
            Rt(Time.deltaTime);
            Rotation(Dir, camera);
        }
        #region Body transformations
        private void Move(Vector2 Dir)
        {
            if (Rolling)
            {
                rb.velocity = R_pos * speed * 4;
                if (r_l <= 0)
                    Rolling = false;
            }
            else
                rb.velocity = Dir.normalized * speed * ss;
        }
        private void Rotation(Vector2 Dir, Vector3 Camera)
        {
            //body
            if (Dir != Vector2.zero)
            {
                float body_angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
                body.rotation = Quaternion.AngleAxis(body_angle, Vector3.forward);
            }
            //head
            if (!Wall)
            {
                Vector3 pos = Camera - transform.position;
                float head_angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                head.rotation = Quaternion.AngleAxis(head_angle - 90f, Vector3.forward);
            }
        }
        #endregion
        #region Abilities
        private void Sneak(bool a)
        {
            ss = a ? 0.5f : 1f;
            Sneaking = a;
        }
        private void Roll(bool a, Vector2 Dir)
        {
            if (a && r_cool <= 0)
            {
                r_cool = 2;
                r_l = 0.5f;
                Rolling = true;
                R_pos = Dir.normalized;
            }
        }
        private void Turn_ToWall(Transform Wall, Vector3 jopa)
        {
            if (w_cool <= 0)
            {
                jopa = Wall.InverseTransformPoint(jopa);
                bool a = Mathf.Abs(jopa.x) - 0.5f > Mathf.Abs(jopa.y) - 0.5f;
                jopa = Round(jopa);
                float head_angle = Wall.rotation.eulerAngles.z;
                Vector2 hel = Wall.InverseTransformPoint(transform.position);
                if (a)
                {
                    if (jopa.x == 0.5f)
                        head_angle -= 90;
                    else
                        head_angle += 90;
                    jopa.y = hel.y;
                }
                else
                {
                    if (jopa.y == -0.5f)
                        head_angle += 180;
                    jopa.x = hel.x;
                }
                transform.position = Wall.TransformPoint(jopa);
                head.rotation = Quaternion.AngleAxis(head_angle, Vector3.forward);
            }
        }
        #endregion
        #region Collisions
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rolling = false;
            if (collision.transform.tag == "Solid")
            {
                Turn_ToWall(collision.transform, collision.contacts[0].point);
                Wall = true;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.tag == "Solid")
            {
                Wall = false;
                w_cool = 0.04f;
            }
        }
        #endregion
        #region Helper functions
        private void Rt(float tm)
        {
            if (r_cool > 0) r_cool -= tm;
            if (r_l > 0) r_l -= tm;
            if (w_cool > 0) w_cool -= tm;
        }
        public Vector2 Round(Vector2 value)
        {
            return new Vector2(Round(value.x, 1), Round(value.y, 1));
        }
        public float Round(float f, int d)
        {
            int coef = f < 0 ? -1 : 1;
            f *= coef;
            float power = Mathf.Pow(10, -d);
            float h = f - f % power;
            if (f % power >= 0.5f * power)
                h += power;
            return h *= coef;
        }
        #endregion
    }
}
