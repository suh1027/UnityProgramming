using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;
    // 피격시 색이 살짝 변하는 sprites 구현하기위해

    SpriteRenderer spriteRenderer;
    Rigidbody2D r2d;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        r2d = GetComponent<Rigidbody2D>();
        r2d.velocity = Vector2.down * speed; //아래로 내려오도록 설정
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        
        Invoke("ReturnSprite", 0.1f); // 시간차(0.1초)를 두어 ReturnSprite()

        if(health <= 0)
        {
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
}
