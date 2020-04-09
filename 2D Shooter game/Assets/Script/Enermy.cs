using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;
    public string enermyName;
    // 피격시 색이 살짝 변하는 sprites 구현하기위해

    public float maxShotDelay;
    public float curShotDelay;

    public int enermyScore;

    public GameObject bulletA;
    public GameObject bulletB;

    public GameObject ItemCoin;
    public GameObject ItemPower;
    public GameObject ItemBoom;

    public GameObject player;
    

    SpriteRenderer spriteRenderer;
    //Rigidbody2D r2d;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //r2d = GetComponent<Rigidbody2D>();
        //r2d.velocity = Vector2.down * speed; //아래로 내려오도록 설정
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;
        
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        
        Invoke("ReturnSprite", 0.1f); // 시간차(0.1초)를 두어 ReturnSprite()

        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enermyScore;

            //#. Random Ratio Drop Item
            int ran = Random.Range(0, 10);

            if(ran < 3)
            {
                Debug.Log("No Item");
            }
            else if(ran < 6) //Coin
            {
                Instantiate(ItemCoin, transform.position, ItemCoin.transform.rotation);
            }
            else if(ran < 8) //Power
            {
                Instantiate(ItemPower, transform.position, ItemPower.transform.rotation);
            }
            else if(ran < 10) //Boom
            {
                Instantiate(ItemBoom, transform.position, ItemBoom.transform.rotation);
            }

            Destroy(gameObject);

        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            //맞았을때 총알 제거
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        //총알 충돌하지 않도록 isTrigger 키기
        //Instantiate 매개변수 오브젝트를 생성하는 함수
        
        if (curShotDelay < maxShotDelay)
            return;

        if(enermyName == "S")
        {
            GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
            Rigidbody2D r2d = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            r2d.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }

        else if (enermyName == "L")
        {
            GameObject bulletR = Instantiate(bulletB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletB, transform.position + Vector3.left * 0.3f, transform.rotation);
            
            
            Rigidbody2D r2dR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D r2dL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.right * 0.3f);

            r2dR.AddForce(dirVecR.normalized * 2, ForceMode2D.Impulse);
            r2dL.AddForce(dirVecL.normalized * 2, ForceMode2D.Impulse);
            //단위 벡터 만드는방법 .normalized 크기는 1로 단위벡터화
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime; //현재시간
    }


}
