using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerManager : MonoBehaviour
{

    public float remainingTime = 60;
    public bool finished;
    private GameObject[] Players;
    private Text Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = GameObject.Find("Timer_Display").GetComponent<Text>();
        finished = false;
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update(){

        if(!finished){
            if (remainingTime > 0){
                remainingTime -= Time.deltaTime;    
                Timer.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(remainingTime / 60), Mathf.FloorToInt(remainingTime % 60));
            }else{
                string winner = (Players[0].GetComponent<ScoreManager>().getScore()>Players[1].GetComponent<ScoreManager>().getScore()) ? "Team PURPLE" : "Team BLUE";
                winner = (Players[0].GetComponent<ScoreManager>().getScore() == Players[1].GetComponent<ScoreManager>().getScore()) ? "DRAW" : winner;

                GameOver(winner);

            }
        }
        
    }

    void GameOver(string winner){
        Players[0].GetComponent<MoveWithKeyboardBehavior>().stop();
        Players[1].GetComponent<MoveWithKeyboardBehavior>().stop();
        GameObject.FindGameObjectWithTag("Sheep").GetComponent<GhostSheepBehavior>().stop();


        GameObject.Find("GameOverScreen").GetComponent<Image>().enabled = true;
        if(winner == "DRAW"){
            GameObject.Find("GameOverText").GetComponent<Text>().text = "Game Over \nDRAW";
            
        }else{
            GameObject.Find("GameOverText").GetComponent<Text>().text = "Game Over \n" + winner;
        }

        GameObject.Find("GameOverText").GetComponent<Text>().enabled = true;
    
    }

}
