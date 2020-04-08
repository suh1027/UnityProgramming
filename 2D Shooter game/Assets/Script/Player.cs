using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public 변수
    public float speed;
    //bullet 속도제한 변수
    public float maxShotDelay; // 최대
    public float curShotDelay; // 현재
    public float power;

    public bool isTouchTop;
    public bool isTouchBot;
    public bool isTouchLeft;
    public bool isTouchRight;

    public GameObject bulletA;
    public GameObject bulletB;
    
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBot))
            v = 0;

        //Time.deltaTime을 붙이는 이유 -> 컴퓨터환경에구에받지않고 동등한 이동거리계산을 위한 ... 프레임 관련 
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            animator.SetInteger("Input", (int)h);

    }

    void Fire()
    {
        //총알 충돌하지 않도록 isTrigger 키기
        //Instantiate 매개변수 오브젝트를 생성하는 함수
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                //power = 1
                GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
                Rigidbody2D r2d = bullet.GetComponent<Rigidbody2D>();
                r2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                //power = 2
                //power = 1일때
                GameObject bulletR = Instantiate(bulletA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D r2dR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D r2dL = bulletL.GetComponent<Rigidbody2D>();
                r2dR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                r2dL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletB, transform.position, transform.rotation); //가운데를 크게! BulletBtype 사용
                GameObject bulletLL = Instantiate(bulletA, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D r2dRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D r2dCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D r2dLL = bulletLL.GetComponent<Rigidbody2D>();
                r2dRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                r2dCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                r2dLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;


        }


        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime; //현재시간
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBot = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBot = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
