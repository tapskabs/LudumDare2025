using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2.0f;

    void Update()
    {

        if(GetInteractableObject() != null) //Interactable Object reveals itself by having child reveal itself
        {
            GameObject pickUp = GetInteractableObject().gameObject;
            if(pickUp.GetComponent<StatPickup>().HasAccepted == false)
            {
                pickUp = pickUp.transform.GetChild(0).gameObject;
                pickUp.SetActive(true);
            }
            else
            {
                pickUp = pickUp.transform.GetChild(0).gameObject;
                pickUp.SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out StatPickup interactor) && !interactor.HasAccepted)
                {
                    interactor.Interact();
                }
            }
        }
    }//End of update

    public StatPickup GetInteractableObject()
    {
        float interactRange = 2;
       // Debug.Log("GetObject running");

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //Debug.Log(collider.name);
            if (collider.TryGetComponent(out StatPickup interactor))
            {
                return interactor;
            }
        }

        return null;
    }

    

}
