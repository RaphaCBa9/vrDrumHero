using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnPosition : MonoBehaviour
{
    public Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("Collider component not found on the GameObject.");
            return;
        }

        // Set the collider to be a trigger
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Note"))
        {
            Debug.Log("Note object entered the trigger zone.");
            // Destroy the object
            Destroy(other.gameObject);
        }
    }
}
