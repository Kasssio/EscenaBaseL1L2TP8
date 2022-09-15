using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Secreto : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("hola?"))
        {
            SceneLoad("secreto");
        }
    }

    void SceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
