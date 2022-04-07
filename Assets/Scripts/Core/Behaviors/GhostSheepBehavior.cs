using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    

public GameObject[] Players;

    public void Start(){
    }
    public override Steering GetSteering()
    {
        
        Steering steering = new Steering();

        Players = GameObject.FindGameObjectsWithTag("Player");
        
        Vector3 position = transform.position;

        Vector3 Player1position = Players[0].transform.position;
        Vector3 diff1 = Player1position - position;
        float curDistance1 = diff1.sqrMagnitude;

        Vector3 Player2position = Players[1].transform.position;
        Vector3 diff2 = Player2position - position;
        float curDistance2 = diff2.sqrMagnitude;

        float horizontal = 0 ;
        float vertical = 0 ;

        steering.linear = new Vector3(horizontal, 0, vertical)* agent.maxAccel ;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel)) ;

        return steering;
    }



}
