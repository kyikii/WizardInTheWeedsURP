using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacementNode : MonoBehaviour
{
    //public Material NodeMat,BaseSpriteMat;

    [SerializeField] private Sprite ActiveSprite,InactiveSprite;
    
    

    //bool hovering = false;
    public bool occupied, disabled = true;
    //int spriteIndex = 0;
    private float speed = 6.0f, CheckInerval = 0f;
    [SerializeField] float minSpeed;
    
    private float journeyLength = 0, distCovered = 0, startTime;
    private bool lerp=false, swap=false;
    private Vector3 start,end,HoldPoint;
    [SerializeField] private GameObject Player, Manager;
    [SerializeField] private GameObject OccupiedObj;
    //private SpriteRenderer BaseSprite;


    // Start is called before the first frame update
    void Start()
    {

        //BaseSprite = GetComponentInChildren<SpriteRenderer>();
        Player = GameObject.Find("PlayerController");
        Manager = GameObject.Find("NodeManager");
        //Reticle = GameObject.Find("Reticle");
        //Glyphs = GameObject.Find("GlyphHolder");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(lerp)
        {
          LerpObjTo();  
        }
        
        switch(CheckInerval)
        {
            case < 35f:
            CheckInerval ++;
            //Debug.Log(CheckInerval);
            break;

            case 35f:
            distancecheck();
            break;

        }
    }

    private void distancecheck()
    {
        CheckInerval = 0;
        float PlayerDist;
        PlayerDist = Vector3.Distance(this.transform.position,Player.transform.position);
        //Debug.Log(PlayerDist);
        if(PlayerDist < 3.0f)
        {
            disabled = false;
        }
        else
        {
            disabled = true;
            GetComponentInChildren<SpriteRenderer>().sprite = InactiveSprite;
            switch(occupied)
            {
                case true:
                UnhighlightObj(OccupiedObj);
                break;

                case false:
                if(Manager.GetComponent<PlayerManager>().heldObject != null)
                {
                    UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                }
                break;
            }
        }
    }

    private void OnMouseOver()
    { 
        if(!disabled && !lerp)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = ActiveSprite;
          
            //if(OccupiedObj.layer !=31 && OccupiedObj !=null)
            
            switch(occupied)
            {
                case true:
                highlightObj(OccupiedObj);
                highlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
                case false:
                highlightObj(Manager.GetComponent<PlayerManager>().heldObject);
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
    }
    private void OnMouseExit()
    {  
        if(!disabled)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = InactiveSprite;
            

            switch(occupied)
            {
                case true:
                UnhighlightObj(OccupiedObj);
                UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
                case false:
                UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
            }

            //spriteIndex = 0;
            //Reticle.SendMessage("updateUIState",spriteIndex);
            
        }
        
    }

    private void highlightObj(GameObject TargetObj)
    {
        if(TargetObj != null)
        {
            foreach(Transform child in TargetObj.transform)
            {
                if(child.gameObject.tag != "Particle")
                {
                    child.gameObject.layer = 31;
                }
            
            }
        }
    }

    private void UnhighlightObj(GameObject TargetObj)
    {
        if(TargetObj != null)
        {
            foreach(Transform child in TargetObj.transform)
            {
                if(child.gameObject.tag != "Particle")
                {
                    child.gameObject.layer = 0;
                }
            
            }
        }
        
    }

    void OnTriggerStay(Collider other)
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
        //pick something up
        if (!Manager.GetComponent<PlayerManager>().holding && occupied && !disabled && !lerp)
        {
            //gameObject.GetComponentInParent<PlayerManager>().PickUp.Play();
            PickUpSeq();
        }
        //put something down
        else if(Manager.GetComponent<PlayerManager>().holding && !occupied && !disabled && !lerp)
        {
            //gameObject.GetComponentInParent<PlayerManager>().PutDown.Play();
            PutDownSeq();
        }
        //Swap two items
        else if(Manager.GetComponent<PlayerManager>().holding && occupied && !disabled && !lerp && !swap)
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

    /*public void updateDistanceCheck()
    {
        float PlayerDist;
        PlayerDist = Vector3.Distance(this.transform.position,Player.transform.position);
        //Debug.Log(PlayerDist);
        if(PlayerDist < 3.0f)
        {
            disabled = false;
        }
        else
        {
            disabled = true;
            GetComponentInChildren<SpriteRenderer>().sprite = InactiveSprite;
            switch(occupied)
            {
                case true:
                UnhighlightObj(OccupiedObj);
                break;
                case false:
                UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
                break;
            }
        }
    }*/

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
            if(swap)
            {
                PutDownSeq();
                swap = false;
                EndLerpCheck();
            }
            else
            {
                EndLerpCheck();
            }            
        }
    }

    private void EndLerpCheck()
    {
        //OccupiedObj = null;
        //UnhighlightObj(Manager.GetComponent<PlayerManager>().heldObject);
        Player.GetComponent<Player_Controller>().CamLock = false;
    }

    private void PickUpSeq()
    {
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
