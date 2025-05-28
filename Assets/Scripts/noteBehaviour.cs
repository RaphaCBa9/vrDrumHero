using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteBehaviour : MonoBehaviour
{
    public bool canHit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Drum"))
        {
            //Debug.Log("Nota entrou na área de acerto");
            canHit = true;

            if (other.gameObject.GetComponent<noteEvaluation>().hasHit == true)
            {
                Destroy(gameObject);
                //Debug.Log("Nota acertada!");    
                GameManager.hitNote();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drum"))
        {
            //Debug.Log("Nota saiu da área de acerto");

            canHit = false;
        }
    }
    
}
