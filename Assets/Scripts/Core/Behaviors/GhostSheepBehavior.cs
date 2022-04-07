using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    
    public void Start(){
        gameObject.tag = "Sheep";
    }
    public override Steering GetSteering()
    {
        
        Steering steering = new Steering();
        GameObject.FindGameObjectsWithTag("Player");
        return steering;
    }



}
