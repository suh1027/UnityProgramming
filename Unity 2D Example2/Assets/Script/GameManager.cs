using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] stages;

    //UI
    

    public void NextStage()
    {
        if(stageIndex < stages.Length-1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();
        }

        else
        {
            //Player control lock
            Time.timeScale = 0;
            //result UI
            Debug.Log("게임 클리어!");
            //Restart Button UI

        }

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (health > 1) {
                PlayerReposition();
            }
            HealthDown();
            
        }
    }
    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
        }
        else
        {
            // Player Die Effect
            player.OnDie();

            // Result UI
            Debug.Log("죽었습니다.(임시UI)");

            // Retry Button UI

        }
    }
    public void PlayerReposition()
    {
        player.transform.position = new Vector3(-6, 3, 0);
        player.VelocityZero();
    }

}
