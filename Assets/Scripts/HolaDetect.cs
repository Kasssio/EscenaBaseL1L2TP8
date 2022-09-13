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

    GameObject[] computadoras;

    public bool HasRepairedComputer = false;
    private bool ComputerHasBeenSelected = false;

    private string[] NPCDialogue = null; // solo hay q cambiar el dialogo cuando repare la compu 
    
    private int IntroMsgIndex = 0;
    
   
    // Start is called before the first frame update
    void Start()
    {
        UIElements.SetActive(false);
        computadoras = GameObject.FindGameObjectsWithTag("Monitor");

      
    }

    // Update is called once per frame
    void Update()
    {
        AvanzarDialogo();
    }


    void OnTriggerEnter(Collider col)
    { //yo cavernicola, yo chocar contra pared ??? por lo de 


        if(col.gameObject.GetComponent<NPCDialogue>() && !HasRepairedComputer)
        {
            NPCDialogue = col.gameObject.GetComponent<NPCDialogue>().data.dialogueLines;
            UIElements.SetActive(true);

            dialogueTxt.text = NPCDialogue[IntroMsgIndex];
        }


        else if(col.gameObject.GetComponent<NPCDialogue>() && HasRepairedComputer)
        {
            //cambiar al dialogo q ya la reparo. acordate de cambiar el booleano
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
        if (Input.GetKeyDown(KeyCode.E) && dialogueTxt.IsActive() && !HasRepairedComputer)
        {
            if(IntroMsgIndex < 2 && !ComputerHasBeenSelected)               
            {
                dialogueTxt.text = NPCDialogue[++IntroMsgIndex];
            }

             if(IntroMsgIndex >= 2)
             {
                
                ComputerHasBeenSelected = MonitorRandom(); //si
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

        return true;
    }
}
//veni a ds que quiero entender, no que me lo hagas :( xd