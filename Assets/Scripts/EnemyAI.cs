using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum state { Wander, RushPlayer, Attack};

    [Header("AI & Movement")]
    public Transform player;
    public float wanderDistance = 10.0f;
    public float rushPlayerDistance = 5.0f;
    public float attackDistance = 1.0f;
 
    public float walkSpeed = 1.0f;
    public float runSpeed = 5.0f;

    private float originalY;
    private state currentState = state.Wander;
    private Vector3 wanderTarget;

    [Header("Enemy Stats")]
    public float health = 10.0f;
    public float damage;


    private void Start()
    {
        wanderTarget = transform.position + new Vector3((Random.insideUnitCircle * wanderDistance).x, 0, (Random.insideUnitCircle * wanderDistance).y);
        originalY = this.transform.position.y;

    }

    private void Update()
    {
        switch(currentState)
        {
            
            case state.Wander:
                //move towards the Wander Point
                transform.position = Vector3.MoveTowards(transform.position, wanderTarget, walkSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, wanderTarget) < 1f)
                {
                    wanderTarget = transform.position + new Vector3((Random.insideUnitCircle * wanderDistance).x, 0, (Random.insideUnitCircle * wanderDistance).y);
                }
                //Checks if the Enemy is close to the Player so it can begin running towards the player
                if (Vector3.Distance(transform.position, player.position) < rushPlayerDistance)
                {
                    currentState = state.RushPlayer;
                }
                break;

                case state.RushPlayer:
                    //Runs Towards the Player AKA Rushing the Boiii

                    transform.position = Vector3.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);
                    float Distance = Vector3.Distance(transform.position, player.position);

                    if(Distance < attackDistance)   //If we have have the PLayer in Attack Distance
                    {
                        currentState = state.Attack;
                        //attackTimer = 0f;
                    }
                    else if (Distance > wanderDistance)
                    {
                        currentState = state.Wander;
                    }
                    break;

                case state.Attack:

                    //Add in Attack Stuff
                    if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z)) > attackDistance)
                    {
                        currentState = state.RushPlayer;
                    }
                    break;

        }//end Switch

    }// end Update


    public void TakeDamage()
    {
        //Use Singleton PlayerStats to Access Damage that the Player Does.
    }
}
