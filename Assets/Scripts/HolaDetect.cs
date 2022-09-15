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

    GameObject CompSeleccionada;
    GameObject[] computadoras;

    public bool HasRepairedComputer = false;
    [SerializeField] bool ComputerHasBeenSelected = false;
    [SerializeField] bool EnRango;

    private string[] NPCDialogue = null; // solo hay q cambiar el dialogo cuando repare la compu 
    private string[] FinalNPCDialogue = null;
    
    private int IntroMsgIndex = 0;
    private int FinalMsgIndex = 0;
    
   
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
        if(EnRango && Input.GetKeyDown(KeyCode.F))
        {
            CompuRara.SetActive(false);
            HasRepairedComputer = true;
            CompSeleccionada.GetComponent<AudioSource>().Stop();
        }
    }


    void OnTriggerEnter(Collider col)
    {  
        if(col.gameObject.GetComponent<NPCDialogue>() && !HasRepairedComputer)
        {
            NPCDialogue = col.gameObject.GetComponent<NPCDialogue>().data.dialogueLines;
            FinalNPCDialogue = col.gameObject.GetComponent<NPCDialogue>().data.finalDialogue;
            UIElements.SetActive(true);

            dialogueTxt.text = NPCDialogue[IntroMsgIndex];
        }

        if(col.gameObject.GetComponent<NPCDialogue>() && HasRepairedComputer)
        {
            UIElements.SetActive(true);
            dialogueTxt.text = FinalNPCDialogue[FinalMsgIndex];
        }

        if (col.gameObject.CompareTag("CompuRota") && HasRepairedComputer == false)
        {
            CompuRara.SetActive(true);
        }

        else if (col.gameObject.CompareTag("CompuRota") && HasRepairedComputer == true)
        {
            CompuRara.SetActive(false);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("CompuRota"))
        {
            EnRango = true;
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
            EnRango = false;
        }
    }

    public void AvanzarDialogo()
    {
        #region Unrepaired Branch
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

        #endregion



        if (Input.GetKeyDown(KeyCode.E) && dialogueTxt.IsActive() && HasRepairedComputer && FinalMsgIndex <= FinalNPCDialogue.Length)
        {
            //if user presses E, dialogue box is showing, computer has been repaired and we're in range of the dialogue arr

            dialogueTxt.text = FinalNPCDialogue[++FinalMsgIndex];         

        }
    }


    bool MonitorRandom()
    {
         CompSeleccionada = computadoras[Random.Range(0, computadoras.Length)];

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
