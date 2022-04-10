using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private GameObject [] Players;
    private Text Score;

    void Start()
    {
        GameObject Score_Text = GameObject.Find("Score_Display");
        
        Score = Score_Text.GetComponent<Text>();
        Players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("trouv'e l' objet : " + Players[0] + " et " + Players[1]);
        Score.text = "0 - 0";
    }

    // Update is called once per frame
    void Update()
    {
        Score.text  = Players[0].GetComponent<ScoreManager>().getScore() + " - " + Players[1].GetComponent<ScoreManager>().getScore();
    }
}
