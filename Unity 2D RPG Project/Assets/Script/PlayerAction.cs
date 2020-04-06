using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameManager manager;
    public float speed;

    float h;
    float v;
    bool isHorizonMove;//대각선 이동을 없에기 위한 flag

    Vector3 dirVec;//현재 바라보고 있는 방향값을 가진 변수
    
    GameObject scanObject;
    Rigidbody2D r2d;
    Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //scanObject = GetComponent<GameObject>();

        //animator.SetBool("isChange", true);
    }

    // Update is called once per frame
    void Update() 
    {
        try
        {
            h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
            v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

            bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
            bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");

            //두개 동시에 누르다가 하나 떘을때 이동을 멈추는 현상을 고치기 위해
            bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
            bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

            if (hDown)
            {
                isHorizonMove = true;
            }
            else if (vDown)
            {
                isHorizonMove = false;
            }
            else if (hUp || vUp)
            {
                isHorizonMove = h != 0; // ?????


            }

            //지속적으로 update해서 애니메이션이 실행 X?? 12 30 ~ ch??
            //이해부족
            //     animator.SetBool("isChange", true);
            //궂이 isChange를 써야하는 이유??


            //위를 눌르고 있을때 양옆을 제어시에 
            //애니메이션이 변경되지 않는 문제 발생


            if (animator.GetInteger("hAxisRaw") != h)
            {
                animator.SetInteger("hAxisRaw", (int)h);
                //animator.SetBool("isChange", true);
            }

            //  animator.SetBool("isChange", true);
            else if (animator.GetInteger("vAxisRaw") != v)
            {
                animator.SetInteger("vAxisRaw", (int)v);
                //animator.SetBool("isChange", true);
            }




            //Direction
            if (vDown && v == 1)
            {
                dirVec = Vector3.up;
            }
            else if (vDown && v == -1)
            {
                dirVec = Vector3.down;
            }
            else if (hDown && h == 1)
            {
                dirVec = Vector3.right;
            }
            else if (hDown && h == -1)
            {
                dirVec = Vector3.left;
            }


            // Scan Object
            // NullReferenceException이 나는데 ScanObject 의 FixedUpdate에서의 값이 안먹히는건지??
            // Debug로 확인시 스페이스 한번 클릭당 두번의 ScanObjectName이 출력되는 현상이 발생-> Why??

            if (Input.GetButtonUp("Jump") && scanObject != null) //Space 바 입력
            {
                Debug.Log("This is : " + scanObject.name);
                try
                {
                    manager.UI_Action(scanObject);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }

            }
        }
        catch(Exception e) {}


    }
    void FixedUpdate()
    {
        Vector2 moveVec =
            isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        r2d.velocity = moveVec * 2;

        //Ray

        Debug.DrawRay(r2d.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = 
            Physics2D.Raycast(r2d.position,dirVec,
            0.7f,
            LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
        
        
    }

}
