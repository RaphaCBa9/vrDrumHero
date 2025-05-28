using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intantiateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    GameObject[] cubes = new GameObject[512];
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        int inverted = -1;
        if (this.gameObject.tag == "inverted")
        {
            inverted = 1;
        }
        for (int i = 0; i < cubes.Length; i++)
        {
            GameObject cubeInstance = (GameObject)Instantiate(cubePrefab);
            cubeInstance.transform.position = this.transform.position;
            cubeInstance.transform.parent = this.transform;
            cubeInstance.name = "Cube" + i;

            this.transform.eulerAngles = new Vector3(0, -0.703125f * i * inverted, 0);
            cubeInstance.transform.position = Vector3.forward * 10;

            cubes[i] = cubeInstance;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] != null)
            {
                cubes[i].transform.localScale = new Vector3(0.1f, (AudioPeer.samples[i] * maxScale) * 2, 0.1f);
            }
            
        }
    }
}
