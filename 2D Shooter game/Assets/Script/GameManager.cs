using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//scene 관리를 위해..-> Retry버튼에서

// Instantiate , Destroy

// #오브젝트 풀링? -> Instantiate , Destroy 메모리를 계속 차지하면서 쓰레기 메모리를 남기게 된다.
// 메모리가 쌓이면서 유니티 엔진 자체에서 GC(가비지컬렉트) (쌓인 조각난 메모리를 비우는 기술)
// 가비지컬렉트를 피하기 위해 오브젝트 풀링 기술은 필수 -> 최적화의 기본

public class GameManager : MonoBehaviour
{
    //Prefab 변수화
    public string[] enermyObjs;
    public Transform[] spawnPoints; // 스폰될 위치를 transform 배열에 저장

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    
    //UI 변수
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    public ObjectManager objectManager;

    void Awake()
    {
        enermyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnermy();
            maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        //UI score Update

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); //3자리마다 콜론으로 끊어주는..

    }

    void SpawnEnermy()
    {
        int ranEnermy = UnityEngine.Random.Range(0, 3); // L M S 비행기 세가지 종류
        int ranPoint = UnityEngine.Random.Range(0, 9); //다섯가지 포인트 + 4가지 우측좌측 추가

        //프리팹 생성
        GameObject enermy = objectManager.MakeObj(enermyObjs[ranEnermy]);
        /*Instantiate(enermyObjs[ranEnermy],
                      spawnPoints[ranPoint].position,
                      spawnPoints[ranPoint].rotation);*/
        enermy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D r2d = enermy.GetComponent<Rigidbody2D>();
        Enermy enermyLogic = enermy.GetComponent<Enermy>();
        enermyLogic.player = player;
        enermyLogic.objectManager = objectManager;

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
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe",2f);
        
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4f;
        player.SetActive(true);

        Player PlayerLogic = player.GetComponent<Player>();
        PlayerLogic.isHit = false;
    }

    public void UpdateLifeIcon(int life)
    {
        for (int i = 0; i < 3; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        for (int i = 0; i < 3; i++)
        {
            boomImage[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < boom; i++)
        {
            boomImage[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(0);
    }
}
