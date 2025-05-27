using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerBlinkMaterial : MonoBehaviour
{
    private Material m;
    // Start is called before the first frame update
    public GameObject drumSkin;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Acertei o trigger");
        if (other.gameObject.CompareTag("Drumstick"))
        {
            //Debug.Log("Reconheci a tag");

            m = drumSkin.GetComponent<Renderer>().material;

            if (m == null)
            {
                //Debug.Log("Material não encontrado");
            }
            else
            {
                //Debug.Log("Material encontrado");
            }
            m.SetFloat("_TimeStart", Time.time);
        }
    }
}

