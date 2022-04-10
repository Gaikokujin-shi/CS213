using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{

    public GameObject audioWin;
    private GameObject [] Players;
    // Start is called before the first frame update
    void Start()
    {
       audioWin = GameObject.Find("Audio Gain Point");
       Players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider other){ 
        
        if(other.transform.parent.gameObject.CompareTag("Sheep")){
            if(!other.transform.parent.gameObject.GetComponent<GhostSheepBehavior>().isGhost()){
                audioWin.GetComponent<AudioSource>().Play();
                if(Vector3.Distance(other.transform.position, Players[0].transform.position) < Vector3.Distance(other.transform.position, Players[1].transform.position)){
                    Players[0].GetComponent<ScoreManager>().updateScore(1);
                }else{
                    Players[1].GetComponent<ScoreManager>().updateScore(1);
                }
            }
            
        }
    }
}
