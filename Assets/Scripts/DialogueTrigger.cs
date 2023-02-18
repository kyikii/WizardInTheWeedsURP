using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Flowchart PreferedChart;
    [SerializeField] GameObject PreferedDialogueBox;

    [SerializeField] Player_Controller player;

    [SerializeField] GameObject HeadPoint; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.CamLock = true;
            PreferedChart.SendFungusMessage("Start Dialogue");
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
