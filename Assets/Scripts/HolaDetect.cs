using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolaDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Monitor")
        {
            Debug.Log("HOLA :)");
        }

        if (col.gameObject.name == "JeroPrisma")
        {
            Debug.Log("Blender");
        }

    }
}
