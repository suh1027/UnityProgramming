using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public 변수
    public float speed;
    public bool isTouchTop;
    public bool isTouchBot;
    public bool isTouchLeft;
    public bool isTouchRight;
    
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((h == 1 && isTouchRight)||(h == -1 && isTouchLeft))
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
