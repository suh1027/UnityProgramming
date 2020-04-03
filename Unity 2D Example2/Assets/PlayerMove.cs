using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float maxJump;
    SpriteRenderer sr;
    Rigidbody2D r2d;
    Animator at;
    // Start is called before the first frame update
    void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        at = GetComponent<Animator>();
    }
    
    // # Stop Speed
    private void Update()
    {
        
        //단발적인 키입력에는 update에서 사용 
        //fixed update 단발적인 키입력에서 씹히는 경우 발생
        //키를 땠을때 속도를 줄임
        if (Input.GetButtonUp("Horizontal"))
        {
            // nomalized 벡터 크기를 1로 만든 상태(단위벡터)
            // 방향구할때 씀
            r2d.velocity = new Vector2(
                r2d.velocity.normalized.x * 0.5f, 
                r2d.velocity.y);
        }

        //Diriction Sprite 애니메이션 Flip
        if (Input.GetButtonDown("Horizontal"))
        {
            //flipx ==> bool변수
            sr.flipX = Input.GetAxisRaw("Horizontal") == -1; //키입력이 음수일때 true => Flip 되게 만든다
        }

        // Walking Animation
        if(Mathf.Abs(r2d.velocity.x) < 0.3)
        {
            at.SetBool("isWalking", false);
        }
        else
        {
            at.SetBool("isWalking", true);
        }
        // Jump
        if (Input.GetButtonDown("Jump") && !at.GetBool("isJumping"))
        {
            r2d.AddForce(Vector2.up * maxJump, ForceMode2D.Impulse);
            at.SetBool("isJumping", true);
        }



    }

    // Move player
    void FixedUpdate()
    {
        // Move Horizontal
        float h = Input.GetAxisRaw("Horizontal");
        r2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // MaxSpeed 제한
        if(r2d.velocity.x > maxSpeed) //Right MaxSpeed
        {
            r2d.velocity = new Vector2(maxSpeed,r2d.velocity.y);
        }
        else if (r2d.velocity.x < maxSpeed * (-1))
        {
            r2d.velocity = new Vector2(maxSpeed * (-1), r2d.velocity.y);
        }

        //걸림방지// 회전불가능 ==> rigidbody freeze rotate z축고정

        // RayCast 오브젝트 검색을 위해 Ray를 쏘는 방식
        // Landing Platform

        Debug.DrawRay(r2d.position,Vector3.down,new Color(0,1,0));
        //에디터상에서만 Ray를 그려주는 함수

        //Ray에 닿은 오브젝트
        RaycastHit2D rayHit = Physics2D.Raycast(
            r2d.position, 
            Vector3.down,
            1,
            LayerMask.GetMask("Platform"));

        if(r2d.velocity.y < 0) //떨어질때만 ray로 false
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.9f)
                {
                    at.SetBool("isJumping", false);
                }
                //Debug.Log(rayHit.collider.name);
            }
        }
        
    }
}
