using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteEvaluation : MonoBehaviour
{
    private Collider triggerZone;

    public bool hasHit = false;

    // coordenates of the center of the collider
    private Vector3 center;
    private Vector3 noteCenter;
    private Vector3 error;

    private bool NoteInZone = false;

    public AudioSource audioSource;

    public int total = 24; // total number of hits allowed

    // Start is called before the first frame update
    void Start()
    {
        triggerZone = GetComponent<Collider>();
        if (triggerZone == null)
        {
            Debug.LogError("Collider not found on the GameObject.");
        }
        else
        {
            //Debug.Log("Collider found on the GameObject.");
        }
        audioSource = GetComponent<AudioSource>();
         
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on the GameObject.");
        }
        else
        {
            //Debug.Log("AudioSource found on the GameObject.");
        }

        center = triggerZone.bounds.center;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
         if(hasHit == true)
        {
            total--;
            if (total < 0)
            {
                total = 24;
                hasHit = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            //Debug.Log("Note entered the hit area");
            NoteInZone = true;
        }
        if (other.gameObject.tag == "Drumstick")
        {
            hasHit = true;

            if (!NoteInZone)
            {
                //Debug.Log("Drumstick missed the note");
                audioSource.Play();
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            //Debug.Log("Note exited the hit area");
            NoteInZone = false;
        }
        

    }

}
