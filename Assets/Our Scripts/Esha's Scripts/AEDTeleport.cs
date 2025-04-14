using UnityEngine;

public class AEDTeleport : MonoBehaviour
{
    
    [SerializeField] private Transform targetLocation;

    
    public void TeleportTo(Transform newLocation)
    {

        transform.position = newLocation.position;
        transform.rotation = newLocation.rotation;

        Debug.Log("AED teleported to: " + newLocation.name);
    }

    // use in case
    public void TeleportToDefault()
    {
        if (targetLocation != null)
            TeleportTo(targetLocation);
        else
            Debug.LogWarning("Default targetLocation not assigned.");
    }
}