using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public int nextMove;

    Rigidbody2D r2d;
    Animator at;
    SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        
        // Invoke (함수, 초) // 5초뒤에 호출
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate() // 1초에 50번 60번
    {
        // # 기본 움직임
        r2d.velocity = new Vector2(
            nextMove, r2d.velocity.y);

        // # 지형 체크
        Vector2 frontVec = new Vector2(
            r2d.position.x + nextMove * 0.2f, 
            r2d.position.y);


        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(
            frontVec,
            Vector3.down,
            1,
            LayerMask.GetMask("Platform"));
        if(rayhit.collider == null)
        {
            Debug.Log("경고!! 앞은 낭떠러지");
            // 이동방향 전환
            /* 
             nextMove = nextMove * (-1);

             CancelInvoke();
             //현재 작동중인 모든 Invoke 함수를 멈추는 함수
             //안정적인 함수구동을 위해..
             Invoke("Think", 5);
             */
            Turn();
        }
                
    }

    // 간단한 AI 몬스터 제작
    void Think()
    {
        //min 값은 포함이 되는데 max값은 포함이 안된다.
        // min <= range < max
        nextMove = Random.Range(-1, 2);

        // WalkSpeed
        at.SetInteger("WalkSpeed", nextMove);

        // SpriteRenderer FlipX
        // 가만히 있지 않을때만 flip되도록 방향 설정
        if (nextMove != 0)
        {
            sr.flipX = nextMove == 1;
        }

        //Think(); // 재귀함수
        float nextThinkTime = Random.Range(-2f, 3f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {

        nextMove = nextMove * (-1);
        sr.flipX = nextMove == 1;
        CancelInvoke();
        //현재 작동중인 모든 Invoke 함수를 멈추는 함수
        //안정적인 함수구동을 위해..
        Invoke("Think", 5);
    }
}
