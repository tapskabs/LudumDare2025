using UnityEngine;

public class StatPickup : MonoBehaviour
{
    

    private Transform pickUp;
    private PlayerInteract player;
    private bool hasAccepted;

    public int statIncrease = 10;       //Not lower than 3

    public bool HasAccepted { get { return hasAccepted; } }

    public void Interact()
    {
        Debug.Log("INTERACTING!!!!!!");
        hasAccepted = true;         //We cannot interact with the GameObject Again

        if(PlayerStats.Instance != null)
        {
            PlayerStats.Instance.IncreaseStat(statIncrease);
        }
        else
        {
            Debug.Log("PlayerStats.Instance is null");
        }

    }



    private void Start()
    {
        pickUp = transform;
        player = new PlayerInteract();
    }

}
