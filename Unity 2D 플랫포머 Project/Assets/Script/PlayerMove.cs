using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float maxJump;

    // Audio Source 변수
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    SpriteRenderer sr;
    Rigidbody2D r2d;
    Animator at;
    CapsuleCollider2D c2d;
    AudioSource audioSource;
    public MonsterMove mMove;

    // Start is called before the first frame update
    void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        at = GetComponent<Animator>();
        c2d = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        mMove = GetComponent<MonsterMove>();
    }
    
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

    // # Stop Speed
    void Update()
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
        if (Input.GetButton("Horizontal"))
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
            PlaySound("JUMP");
        }



    }

    public void VelocityZero()
    {
        r2d.velocity = Vector2.zero;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enermy")
        {
            //몬스터 처치(몬스터보다 위에있고 아래로 낙하중)
            if(r2d.velocity.y < 0 && 
                transform.position.y > collision.transform.position.y)
            {
                gameManager.stagePoint += 100;
                OnAttack(collision.transform);
                PlaySound("ATTACK");
            }

            else { 
            //Debug.Log("플레이어가 맞았습니다!");
                OnDamaged(collision.transform.position);
                
            }
        }    
    }
    public void OnAttack(Transform enermy)
    {
        //point 
        gameManager.stagePoint += 100;

        //reaction force
        r2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //enermy die
        MonsterMove mMove = enermy.GetComponent<MonsterMove>();

        Debug.Log(mMove);
        
        mMove.Damaged();
    }
    void OnDamaged(Vector2 targetPos)
    {
        //타격시 layer를 11로
        gameObject.layer = 11; //layer를 playerDamaged로
        
        //타격시 색
        sr.color = new Color(1, 1, 1, 0.4f); // R G B 투명도

        //타격 효과

        int dirc = 
            transform.position.x - targetPos.x > 0 ? 1 : -1; 
        r2d.AddForce(new Vector2(dirc,1) * 10, ForceMode2D.Impulse);
        //무적모드까지 함수


        //애니메이션 추가
        at.SetTrigger("doDamaged");

        //health Down
        gameManager.HealthDown();

        Invoke("OffDamaged", 3);

        //타격시 효과음
        PlaySound("DAMAGED");

    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        sr.color = new Color(1, 1, 1, 1); // R G B 투명도
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Point를 얻으면서 동전이 사라지도록 설정
        if(collision.gameObject.tag == "item")
        {
            PlaySound("ITEM");

            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
            {
                gameManager.stagePoint += 50;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 100;
            }
            else if (isGold)
            {
                gameManager.stagePoint += 300;
            }
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Finish")
        {
            //Next Stage로 이동
            PlaySound("FINISH");
            gameManager.NextStage();
            
        }

       
    }
    public void OnDie()
    {
        sr.color = new Color(1, 1, 1, 0.4f);
        // 위아래 뒤집기
        sr.flipY = true;
        // Collider Disable
        c2d.enabled = false;
        // Effect Jump
        r2d.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Effect Sount
        PlaySound("DIE");

    }
}
