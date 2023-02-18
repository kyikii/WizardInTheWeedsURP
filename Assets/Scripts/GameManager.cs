using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject NodeManager,WeedManager,Player;
    [SerializeField] Flowchart start_FC,statue_FC,codex_FC;
    [SerializeField] int CastDistance = 5;
    public RaycastHit HoveredObject;

    IEnumerator Raycast()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            UpdateRaycast();
            
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Raycast());
    }

    // Update is called once per frame
    void Update()
    {
        BookActivation();
    }

    void UpdateRaycast()
    {
        Physics.Raycast(Player.transform.position, Player.transform.forward, out HoveredObject, CastDistance);
        Debug.DrawRay(Player.transform.position, Player.transform.forward * CastDistance, Color.red ,10);
        if(HoveredObject.collider == null)
        {
            Debug.Log("Null");
        }
        else
        {
            Debug.Log(HoveredObject.collider.name);
        }
    }

    void BookActivation()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(start_FC.HasExecutingBlocks() == false && statue_FC.HasExecutingBlocks() == false)
            {
                Debug.Log("Do the book");
                codex_FC.SendFungusMessage("ActivateBook");
            }
        }
    }
}
