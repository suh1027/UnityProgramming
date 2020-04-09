using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    //Parallax : 거리에 따른 상대적 속도를 활용한 기술
    //플랫포머에서 자주 쓰이는 기법
    public float speed;

    //배경 스크롤링 구현
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2; // 카메라의 사이즈(높이의 절반)
    }

    void Update()
    {
        Move();
        Scrolling();
    }

    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            Vector3 backSpritesPos = sprites[startIndex].localPosition;
            Vector3 frontSpritesPos = sprites[endIndex].localPosition;

            sprites[endIndex].transform.localPosition = backSpritesPos + Vector3.up * viewHeight;

            //index를 계속 돌리는법
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}

