using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows = 0, 
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public InputKeyboard inputKeyboard;

    public bool stopped = false;

    public override Steering GetSteering()
    {
        if (!stopped){
        
        Steering steering = new Steering();

        float horizontal = Input.GetAxis ($"Horizontal_{inputKeyboard}") ;
        float vertical = Input.GetAxis ($"Vertical_{inputKeyboard}") ;

        steering.linear = new Vector3(horizontal, 0, vertical)* agent.maxAccel ;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel)) ;
        return steering;
        }
        return new Steering();
    }

    public void stop(){
        stopped = true;
    }


}
