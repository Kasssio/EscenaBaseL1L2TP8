using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HolaDetect : MonoBehaviour
{
    [SerializeField] GameObject UIElements;
    [SerializeField] TextMeshProUGUI dialogueTxt;
    [SerializeField] NPCData Datita;
    [SerializeField] AudioClip StickyKeysSound;
    [SerializeField] GameObject CompuRara;


    GameObject[] computadoras;

    public bool HasRepairedComputer = false;
    [SerializeField] bool ComputerHasBeenSelected = false;

    private string[] NPCDialogue = null; // solo hay q cambiar el dialogo cuando repare la compu 
    
    private int IntroMsgIndex = 0;
    
   
    // Start is called before the first frame update
    void Start()
    {
        UIElements.SetActive(false);
        computadoras = GameObject.FindGameObjectsWithTag("Monitor");
        CompuRara.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        AvanzarDialogo();
    }


    void OnTriggerEnter(Collider col)
    {  


        if(col.gameObject.GetComponent<NPCDialogue>() && !HasRepairedComputer)
        {
            NPCDialogue = col.gameObject.GetComponent<NPCDialogue>().data.dialogueLines;
            UIElements.SetActive(true);

            dialogueTxt.text = NPCDialogue[IntroMsgIndex];
        }


        else if(col.gameObject.GetComponent<NPCDialogue>() && HasRepairedComputer)
        {
            
        }

        if (col.gameObject.CompareTag("CompuRota"))
        {
            CompuRara.SetActive(true);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("JeroPrisma"))
        {
            UIElements.SetActive(false);
        }

        if (col.gameObject.CompareTag("CompuRota"))
        {
            CompuRara.SetActive(false);
        }
    }

    public void AvanzarDialogo()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueTxt.IsActive() && !HasRepairedComputer)
        {
            if(IntroMsgIndex < 2 && !ComputerHasBeenSelected)               
            {
                dialogueTxt.text = NPCDialogue[++IntroMsgIndex];
            }

             if(IntroMsgIndex >= 2 && ComputerHasBeenSelected == false)
             {
                
                ComputerHasBeenSelected = MonitorRandom();
                ComputerHasBeenSelected = true;
             }

             

           
        }
    }


    bool MonitorRandom()
    {

        GameObject CompSeleccionada = computadoras[Random.Range(0, computadoras.Length)];

        CompSeleccionada.gameObject.AddComponent<AudioSource>(); 

        CompSeleccionada.GetComponent<AudioSource>().clip = StickyKeysSound;
        CompSeleccionada.GetComponent<AudioSource>().loop = true;
        CompSeleccionada.GetComponent<AudioSource>().maxDistance = 3;
        CompSeleccionada.GetComponent<AudioSource>().minDistance = 1;
        CompSeleccionada.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
        CompSeleccionada.GetComponent<AudioSource>().spatialBlend = 1;
        CompSeleccionada.GetComponent<AudioSource>().Play();


        CompSeleccionada.gameObject.tag = "CompuRota";
        return true;

        


    }
}
