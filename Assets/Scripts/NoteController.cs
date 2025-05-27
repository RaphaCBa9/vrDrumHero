using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float speed;
    public Material mat;

    public Renderer[] model;
    

    private void Start()
    {
        foreach (Renderer renderer in model)
        {
            renderer.material = mat;
        }
    }

    private void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));

        if(transform.position.z < -5f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
         if (other.gameObject.tag == "Drums")
        {
            noteEvaluation noteEvaluation = other.GetComponent<noteEvaluation>();
            print("Inside");
            if (noteEvaluation != null && noteEvaluation.hasHit)
            {
                
                // Destroy the note if the strum was fresh
                Destroy(gameObject);
                
            }
        }
    }
}
