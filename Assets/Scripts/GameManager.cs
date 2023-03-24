using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    //private bool ActivateCodex;
    [SerializeField] GameObject NodeManager,WeedManager,Player;
    public GameObject[] charts;
    GameObject[] menus;
    [SerializeField] Flowchart codex_FC;
    //[SerializeField] MenuDialog MD_A,MD_B;
    [SerializeField] int CastDistance = 5,NumCharts,NumMenus;
    public RaycastHit RayOut;

    Transform PlayerCam;
    public GameObject HoveredObject,NillObj;

    IEnumerator Raycast()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            UpdateRaycast();
            
            if(RayOut.collider == null)
            {
                HoveredObject = NillObj;
            }
            else
            {
                HoveredObject = RayOut.collider.gameObject;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        charts = new GameObject[NumCharts];
        charts = GameObject.FindGameObjectsWithTag("Flowchart");

        menus = new GameObject[NumMenus];
        menus = GameObject.FindGameObjectsWithTag("MenuDialogue");
    
        for(int i = 0; i< NumCharts;i++)
        {
            Debug.Log(charts[i]);
        }
        for(int i = 0; i< NumMenus;i++)
        {
            Debug.Log(menus[i]);
            menus[i].SetActive(false);
        }

        PlayerCam = Player.transform.GetChild(0).transform.GetChild(0);
        
        //Debug.Log(charts[1]);
        HoveredObject = NillObj;
        StartCoroutine(Raycast());
    }

    // Update is called once per frame
    void Update()
    {
        BookActivation();
    }

    void UpdateRaycast()
    {
        Physics.Raycast(PlayerCam.position, PlayerCam.forward, out RayOut, CastDistance);
        Debug.DrawRay(PlayerCam.position, PlayerCam.forward * CastDistance, Color.red ,10);
        if(HoveredObject == NillObj)
        {
            Debug.Log("Null");
        }
        else
        {
            Debug.Log(HoveredObject.name);
        }
    }

    void BookActivation()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && ChartsRunning() == false)
        {
            Debug.Log("Do the book");
            codex_FC.SendFungusMessage("ActivateBook");
            //Player.transform.GetChild(1).transform.GetChild(0).GetComponent<ToolBeltScript>().resetTools();
        }
    }

    public bool ChartsRunning()
    {
        bool ChartsRunning = false;
        for(int i = 0; i< NumCharts;i++)
        {
            Flowchart TempFC = charts[i].GetComponent<Flowchart>();
            if(TempFC.HasExecutingBlocks() == true)
            {
                ChartsRunning = true;
                //codex_FC.SetBooleanVariable
            }
        }

        for(int i = 0; i< NumMenus;i++)
        {
            MenuDialog TempMD = menus[i].GetComponent<MenuDialog>();
            if(TempMD.IsActive())
            {
                ChartsRunning = true;
            }
        }
        return ChartsRunning;
    }
}
