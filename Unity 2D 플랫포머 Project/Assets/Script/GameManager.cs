using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;// 꼭 설정!

public class GameManager: MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] stages;

    //UI
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        if(stageIndex < stages.Length-1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }

        else
        {
            //Player control lock
            Time.timeScale = 0;
            //result UI
            Debug.Log("게임 클리어!");
            //Restart Button UI
            
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            UIRestartBtn.SetActive(true);
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
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //모든 Health UI Off
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);

            // Player Die Effect
            player.OnDie();

            // Result UI
            Debug.Log("죽었습니다.(임시UI)");

            // Retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }
    public void PlayerReposition()
    {
        player.transform.position = new Vector3(-6, 3, 0);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1; // timescale??
        SceneManager.LoadScene(0);
    }

}
