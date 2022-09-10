using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HolaDetect : MonoBehaviour
{
    [SerializeField] GameObject UIElements;
    [SerializeField] TextMeshProUGUI dialogueTxt;
    [SerializeField] string[] NPCDialogue;
    // Start is called before the first frame update
    void Start()
    {
        UIElements.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("JeroPrisma"))
        {
            UIElements.SetActive(true);
            NPCDialogue = col.gameObject.GetComponent<NPCDialogue>().data.dialogueLines;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("JeroPrisma"))
        {
            UIElements.SetActive(false);
        }

    }

    public void AvanzarDialogo()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogueTxt.text = NPCDialogue[1];
        }
            
    }
}
