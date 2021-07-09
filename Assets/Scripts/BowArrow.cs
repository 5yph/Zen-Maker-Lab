using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bow;
    public GameObject projectile;
    public Transform arrowspawn;
    public float speed;

    private Animator animator;
    private SpriteRenderer sprite;

    Projectile projectile_settings;

    [SerializeField] AnimationClip shoot_animation;

    float lastAngle = 0f;
    // float lastAngleRight = 0f;
    // float lastAngleLeft = 0f;
    float shoot_time;
    float fAngle;

    private bool was_facing_right;

    [SerializeField] private float cooldown; // fire rate of projectile, in seconds
    private float next_time_can_fire = 0; // when we can fire the next projectile
    private bool try_to_shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        projectile_settings = projectile.GetComponent<Projectile>();
        // lastAngle = Bow.transform.rotation.z;
        // Debug.Log(Bow.transform.rotation.z);
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;    
        shoot_time = shoot_animation.length / animator.GetFloat("ShootMultiplier");
        Bow.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        was_facing_right = Player.GetComponent<PlayerMovement>().facing_right;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3Pos;

        if (Input.GetMouseButton(0))
        {
            sprite.enabled = true;

            // Project the mouse point into world space at
            //   at the distance of the player.
            v3Pos = Input.mousePosition;
            v3Pos.z = (Player.transform.position.z - Camera.main.transform.position.z);
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            v3Pos = v3Pos - Player.transform.position;
            fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            if (fAngle < 0.0f) fAngle += 360.0f;
            // Debug.Log("1) " + fAngle);

            animator.SetBool("Charging", true);
            // RotateArm(fAngle);

            Bow.transform.Rotate(0, 0, fAngle - lastAngle);
            lastAngle = fAngle;

        } else if (Input.GetMouseButtonUp(0))
        {
            if (next_time_can_fire <= Time.time)
            {
                animator.SetBool("Charging", false);
                animator.SetBool("Shooting", true);
                StartCoroutine("Shoot");
                next_time_can_fire = Time.time + cooldown;
            } else
            {
                try_to_shoot = true;
            }

        } else if (try_to_shoot && (next_time_can_fire <= Time.time)) {
            animator.SetBool("Charging", false);
            animator.SetBool("Shooting", true);
            StartCoroutine("Shoot");
            next_time_can_fire = Time.time + cooldown;
            try_to_shoot = false;
        }

    }

    /*
    void RotateArm(float fAngle)
    {
        if (Player.GetComponent<PlayerMovement>().facing_right)
        {
            Debug.Log("Facing right");
            Bow.transform.Rotate(0, 0, fAngle - lastAngleRight);
            lastAngleRight = fAngle;
        } else
        {
            Debug.Log("Facing left");
            Bow.transform.Rotate(0, 0, fAngle - lastAngleLeft);
            lastAngleLeft = fAngle;
        }
        // lastAngle = fAngle;
    } */

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shoot_time);

        // projectile_settings.velocity.x = -1;

        projectile.GetComponent<Projectile>().velocity.x = speed * Mathf.Cos(fAngle * (Mathf.PI/180));
        projectile.GetComponent<Projectile>().velocity.y = speed * Mathf.Sin(fAngle * (Mathf.PI/180));

        // Debug.Log("Mouse angle:" + fAngle);
        // Debug.Log(projectile.GetComponent<Projectile>().velocity);

        Instantiate(projectile, arrowspawn.position, projectile.transform.rotation);

        sprite.enabled = false;
    }
}
