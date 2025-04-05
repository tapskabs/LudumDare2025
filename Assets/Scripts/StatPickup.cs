using UnityEngine;

public class StatPickup : MonoBehaviour
{
    private Transform pickUp;
    private PlayerInteract player;
    private bool hasAccepted;

    public bool HasAccepted { get { return hasAccepted; } }

    public void Interact()
    {
        Debug.Log("INTERACTING!!!!!!");
        hasAccepted = true;         //We cannot interact with the GameObject Again
    }

    private void Start()
    {
        pickUp = transform;
        player = new PlayerInteract();
    }

}
