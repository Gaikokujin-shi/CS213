using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    
    public GameObject audioSheep;
    public GameObject audioWolf;
    public GameObject audioLoosePoint;
    public GameObject[] Players;
    public bool is_ghost = false;
    public bool is_in_cooldown = false;
    public float range_sheep = 30.0F;
    public void Start(){
        audioSheep = GameObject.Find("Audio Sheep");
        audioWolf = GameObject.Find("Audio Wolf");
        audioLoosePoint = GameObject.Find("Audio Loose Point");

        is_ghost = false;

        Invoke("becomeGhost", Random.Range(10,20));
    }
    public override Steering GetSteering()
    {   
        Players = GameObject.FindGameObjectsWithTag("Player");
        
        Vector3 position = transform.position;

        Vector3 diff1 = Players[0].transform.position - position;
        Vector3 diff2 = Players[1].transform.position - position;

        float curDistance1 = diff1.sqrMagnitude;
        float curDistance2 = diff2.sqrMagnitude;

        Vector3 force = Vector3.zero;
        // Si on est un sheep on fuit
        if (!is_ghost) {
            if (curDistance1 < range_sheep) {
                force -= (diff1 / curDistance1);
            }

            if (curDistance2 < range_sheep) {
                force -= (diff2 / curDistance2);
            }
        }

        // Si on est un ghost on attaque
        if (is_ghost && !is_in_cooldown) {
            GameObject closest = (curDistance1 < curDistance2) ? Players[0] : Players[1];
            Vector3 position_of_closest = closest.transform.position;

            Vector3 diff = position_of_closest - position;
            float distance_target = diff.sqrMagnitude;

            force += (diff / distance_target);       
        }

        force = force.normalized;

        // Update le steering avec le vecteur calculer
        Steering steering = new Steering();

        steering.linear = force * agent.maxAccel ;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel)) ;

        return steering;
    }
    public void becomeGhost() {
        audioWolf.GetComponent<AudioSource>().Play();
        is_ghost = true;
        agent.SetVisualEffect(0, new Color(1, 0, 0), 0);

        Invoke("becomeSheep", Random.Range(10,20));
    }

    public void becomeSheep() {
        audioSheep.GetComponent<AudioSource>().Play();
        is_ghost = false;
        agent.SetVisualEffect(0, new Color(0, 1, 0), 0);

        Invoke("becomeGhost", Random.Range(10,20));
    }

    public bool isGhost(){
        return is_ghost;
    }

    public void coolDown() {
        is_in_cooldown = true;
        Invoke("stopCoolDown", 0.5F);
    }

    public void stopCoolDown() {
        is_in_cooldown = false;
    }

    // Si on fait une collision on change les points et on cooldown
    void OnCollisionEnter(Collision collisionInfo){
        if (is_ghost && !is_in_cooldown) {
            audioLoosePoint.GetComponent<AudioSource>().Play();
            GameObject ennemi = collisionInfo.gameObject;
            ennemi.GetComponent<ScoreManager>().updateScore(-1);
            coolDown();
        }
    }
}