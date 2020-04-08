using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Prefab 변수화
    public GameObject[] enermyObjs;
    public Transform[] spawnPoints; // 스폰될 위치를 transform 배열에 저장

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnermy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnermy()
    {
        int ranEnermy = Random.Range(0, 3); // L M S 비행기 세가지 종류
        int ranPoint = Random.Range(0, 9); //다섯가지 포인트 + 4가지 우측좌측 추가
        
        //프리팹 생성
        GameObject enermy = Instantiate(enermyObjs[ranEnermy],
                                    spawnPoints[ranPoint].position,
                                    spawnPoints[ranPoint].rotation);
        Rigidbody2D r2d = enermy.GetComponent<Rigidbody2D>();
        Enermy enermyLogic = enermy.GetComponent<Enermy>();
        enermyLogic.player = player;

        if (ranPoint == 5 || ranPoint == 6) //Right Spawn
        {
            enermy.transform.Rotate(Vector3.back * 45);
            r2d.velocity = new Vector2(enermyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8) //Left Spawn
        {
            enermy.transform.Rotate(Vector3.forward * 45);
            r2d.velocity = new Vector2(enermyLogic.speed, -1);
        }
        else // Front Spawn
        {
            r2d.velocity = new Vector2(0, enermyLogic.speed*(-1));
        }
    }
}
