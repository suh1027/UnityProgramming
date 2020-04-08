using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject); // Monobehaviour의 기본 메소드 : 오브젝트 삭제
        }
    }
}
