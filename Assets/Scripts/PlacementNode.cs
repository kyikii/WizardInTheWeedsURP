using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementNode : MonoBehaviour
{
    //public Material NodeMat,BaseSpriteMat;

    [SerializeField] private Sprite ActiveSprite,InactiveSprite;
    
    

    //bool hovering = false;
    public bool occupied;
    //int spriteIndex = 0;
    private float speed = 6.0f;
    [SerializeField] float minSpeed;
    
    private float journeyLength = 0, distCovered = 0, startTime;
    private bool lerp=false, swap=false;
    private Vector3 start,end,HoldPoint;
    [SerializeField] private GameObject Player, Manager,GameManager, WeedName;
    [SerializeField] private GameObject OccupiedObj;
    private HighlightScript Highliter;
    private GameManager GM;

    private TMP_Text NameText;
    //private SpriteRenderer BaseSprite;


    // Start is called before the first frame update
    void Start()
    {
        WeedName = GameObject.Find("WeedName");
        Player = GameObject.Find("PlayerController");
        Manager = GameObject.Find("NodeManager");
        GameManager = GameObject.Find("GameManager");
        //Reticle = GameObject.Find("Reticle");

        NameText = WeedName.GetComponent<TMP_Text>();
        Highliter = gameObject.GetComponent<HighlightScript>();
        GM = GameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lerp)
        {
          LerpObjTo();  
        }
    }


    private void OnMouseOver()
    { 
        if(!lerp && GM.HoveredObject.gameObject == this.gameObject)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = ActiveSprite;
            //NameText.text = 

            switch(occupied)
            {
                case true:
                Highliter.highlightObj(OccupiedObj);
                Highliter.highlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
                case false:
                Highliter.highlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
            }
  
            //logic for UI
            /*
            if (!Manager.GetComponent<PlayerManager>().holding && !occupied)
            {
                spriteIndex = 0;
                Reticle.SendMessage("updateUIState",spriteIndex);
            }
            else if (!Manager.GetComponent<PlayerManager>().holding && occupied)
            {
                spriteIndex = 3;
                Reticle.SendMessage("updateUIState",spriteIndex);
            }
            else if (Manager.GetComponent<PlayerManager>().holding && !occupied)
            {
                spriteIndex = 4;
                Reticle.SendMessage("updateUIState",spriteIndex);
            }
            else if (Manager.GetComponent<PlayerManager>().holding && occupied)
            {
                spriteIndex = 5;
                Reticle.SendMessage("updateUIState",spriteIndex);
            }*/

        }
        else if(GM.HoveredObject == GM.NillObj)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = InactiveSprite;
            switch(occupied)
            {
                case true:
                Highliter.UnhighlightObj(OccupiedObj);
                break;

                case false:
                if(Manager.GetComponent<PlayerManager>().heldObject != null)
                {
                    Highliter.UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                }
                break;
            }
        }
    }
    private void OnMouseExit()
    {   
        GetComponentInChildren<SpriteRenderer>().sprite = InactiveSprite;



        switch(occupied)
        {
            case true:
            Highliter.UnhighlightObj(OccupiedObj);
            Highliter.UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
            break;
            case false:
            Highliter.UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
            break;
        }

            //spriteIndex = 0;
            //Reticle.SendMessage("updateUIState",spriteIndex);
            
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Holdable")
        {
            OccupiedObj = other.gameObject;
            occupied = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Holdable")
        {
            occupied = false;
        }
    }

    private void OnMouseDown()
    {
        if(!lerp && GM.HoveredObject.gameObject == this.gameObject)
        {
            //pick something up
            if (!Manager.GetComponent<PlayerManager>().holding && occupied)
            {
                //gameObject.GetComponentInParent<PlayerManager>().PickUp.Play();
                PickUpSeq();
            }
            //put something down
            else if(Manager.GetComponent<PlayerManager>().holding && !occupied)
            {
                //gameObject.GetComponentInParent<PlayerManager>().PutDown.Play();
                PutDownSeq();
            }
            //Swap two items
            else if(Manager.GetComponent<PlayerManager>().holding && occupied && !swap)
            {
            //gameObject.GetComponentInParent<PlayerManager>().PickUp.Play();
                PickUpSeq();
                swap = true;
                Debug.Log("Swap");
            }
            //otherwise null
            else
            {
                Debug.Log("nothing");
            }
        }
    }

    void SetLerpData(Vector3 startpoint, Vector3 endpoint)
    {
        startTime = Time.time;
        start = startpoint;
        end = endpoint;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(start, end);
    }

    void LerpObjTo()
    {
        // Distance moved equals elapsed time times speed..
        distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        OccupiedObj.transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0, 1 ,fractionOfJourney));
        if(OccupiedObj.transform.position == end)
        {
            lerp = false;
            
            switch(swap)
            {
                case true:
                PutDownSeq();
                swap = false;
                //EndLerpCheck();
                break;

                case false:
                EndLerpCheck();
                break;
            }       
        }
    }

    private void EndLerpCheck()
    {
        if(!occupied)
        {
            OccupiedObj = null;
        }
        //UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
        Player.GetComponent<Player_Controller>().CamLock = false;
        if(Manager.GetComponent<PlayerManager>().heldObject != null)
        {
            Manager.GetComponent<PlayerManager>().heldObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void PickUpSeq()
    {
        //Manager.GetComponent<PlayerManager>().heldObject.GetComponent<BoxCollider>().enabled = true;//enables the held item's hitbox
        HoldPoint = Player.gameObject.transform.GetChild(1).gameObject.transform.position; //get the current hold point position
        OccupiedObj.transform.SetParent(Player.transform);                                  
        SetLerpData(this.transform.GetChild(1).gameObject.transform.position,HoldPoint);

        if(!swap)
        {
            Manager.GetComponent<PlayerManager>().holding = true;
            Manager.GetComponent<PlayerManager>().heldObject = OccupiedObj;
        }

        Manager.GetComponent<PlayerManager>().heldObject = OccupiedObj;
        Player.GetComponent<Player_Controller>().currentDirVelocity = new Vector2(0,0);
        Player.GetComponent<Player_Controller>().CamLock =true;
        lerp = true;

        Debug.Log("pick it up");
    }

    private void PutDownSeq()
    {
        Manager.GetComponent<PlayerManager>().heldObject.GetComponent<BoxCollider>().enabled = true;//enables the held item's hitbox
        OccupiedObj = Player.transform.GetChild(2).gameObject; //sets the occupied object to be the 
        HoldPoint = Player.gameObject.transform.GetChild(1).gameObject.transform.position; //get the current hold point position
        Player.transform.GetChild(2).transform.SetParent(this.transform); //set player's 3rd gameobject to be a child of the node it's atatached to
        SetLerpData(HoldPoint,this.transform.GetChild(1).gameObject.transform.position); //set lerp data
        
        if(!swap)
        {
            Manager.GetComponent<PlayerManager>().holding = false;
            Manager.GetComponent<PlayerManager>().heldObject = null;
        }

        Player.GetComponent<Player_Controller>().currentDirVelocity = new Vector2(0,0);
        Player.GetComponent<Player_Controller>().CamLock =true;
        lerp = true;

        Debug.Log("put it down");
    }
}
