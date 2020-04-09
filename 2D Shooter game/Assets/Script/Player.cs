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
    
    public int power;
    public int maxPower;

    public int boom;
    public int maxBoom;
    public bool isBoomTime;

    public bool isTouchTop;
    public bool isTouchBot;
    public bool isTouchLeft;
    public bool isTouchRight;
    
    //예외처리 위한 변수 미사일 두개 동시에 맞았을때 Life가 두번 깎임
    public bool isHit; 
    
    public GameObject bulletA;
    public GameObject bulletB;

    public GameObject boomEffect;

    //UI 관련 변수
    public int life;
    public int score;

    public GameManager gameManager;
    public ObjectManager objectManager;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
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
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                    //Instantiate(bulletA, transform.position, transform.rotation);
                Rigidbody2D r2d = bullet.GetComponent<Rigidbody2D>();
                r2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                
                break;
            case 2:
                //power = 2
                //power = 1일때
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA"); //Instantiate(bulletA, transform.position + Vector3.right * 0.1f, transform.rotation);
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA"); //Instantiate(bulletA, transform.position + Vector3.left * 0.1f, transform.rotation);
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D r2dR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D r2dL = bulletL.GetComponent<Rigidbody2D>();
                
                r2dR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                r2dL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;
            case 3:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");//Instantiate(bulletA, transform.position + Vector3.right * 0.35f, transform.rotation);
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");//Instantiate(bulletB, transform.position, transform.rotation); //가운데를 크게! BulletBtype 사용
                bulletCC.transform.position = transform.position;
                
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");//Instantiate(bulletA, transform.position + Vector3.left * 0.35f, transform.rotation);
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;
                
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

        //피격 이벤트
        
        else if(collision.gameObject.tag == "Enermy"|| collision.gameObject.tag == "EnermyBullet")
        {
            if (isHit)
            {
                return;
            }
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }

        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power >= maxPower)
                    {
                        score += 500;
                        power = maxPower;
                    }
                    else power++;              
                    break;
                case "Boom": // 필살기
                    if (boom >= maxBoom)
                        score += 500;

                    else { 
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
            }

            //아이템 부딛히면 삭제
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
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

    void Boom()
    {
        if (!Input.GetButton("Fire2"))
            return;
        if (isBoomTime)
            return;
        if(boom == 0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);

        //Effect Visible
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        //Remove Enermy
        //FindGameGobject s WithTag => 게임 내에 태그를 가진 오브젝트 들을불러오는 함수 

        //GameObject[] enermyArr = GameObject.FindGameObjectsWithTag("Enermy"); 
        // find 계열 함수는 성능하락을 유발

        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        for (int i = 0; i < enemiesL.Length; i++)
        {
            /*Enermy eneLogic = enermyArr[i].GetComponent<Enermy>();
            eneLogic.OnHit(1000);*/
            if (enemiesL[i].activeSelf)
            {
                Enermy eneLogic = enemiesL[i].GetComponent<Enermy>();
                eneLogic.OnHit(1000);
            }
        }
        for (int i = 0; i < enemiesM.Length; i++)
        {
            if (enemiesM[i].activeSelf)
            {
                Enermy eneLogic = enemiesM[i].GetComponent<Enermy>();
                eneLogic.OnHit(1000);
            }
            //Enermy eneLogic = enermyArr[i].GetComponent<Enermy>();
            //eneLogic.OnHit(1000);
        }
        for (int i = 0; i < enemiesS.Length; i++)
        {
            if (enemiesS[i].activeSelf)
            {
                Enermy eneLogic = enemiesS[i].GetComponent<Enermy>();
                eneLogic.OnHit(1000);
            }
            //Enermy eneLogic = enermyArr[i].GetComponent<Enermy>();
            //eneLogic.OnHit(1000);
        }

        //Remove Bullet
        //GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnermyBullet");

        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");

        for (int i = 0; i < bulletsA.Length; i++)
        {
            if (bulletsA[i].activeSelf){
                bulletsA[i].SetActive(false);
            }
            //Destroy(bullets[i]);
        }
        for (int i = 0; i < bulletsB.Length; i++)
        {
            if (bulletsB[i].activeSelf)
            {
                bulletsB[i].SetActive(false);
            }
            //Destroy(bullets[i]);
        }

    }
}
