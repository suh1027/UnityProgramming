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
        int ranPoint = Random.Range(0, 5); //다섯가지 포인트
        
        //프리팹 생성
        Instantiate(enermyObjs[ranEnermy],
            spawnPoints[ranPoint].position,
            spawnPoints[ranPoint].rotation);
    }
}
