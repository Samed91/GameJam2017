using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { None, CanPickUp, CanInteract, CanTalk }

public class PlayerController : MonoBehaviour
{
    public PlayerStates playerState = PlayerStates.None;
    public Animator anim;
    [Range(0, 10)]
    public float movementSpeed = 3f;
    [Range(0, 5)]
    public float lookSmooth = 3f;
    public float speed;
    public bool enableMovement = false;
    private bool buttonA = false;
    private bool buttonB = false;
    private bool buttonX = false;
    private bool buttonY = false;
    List<GameObject> currentResources = new List<GameObject>();
    public GameObject footPrintPrefab;
    public Transform leftFootPos;
    public Transform rightFootPos;

    void Update()
    {
        UpdateInputs();
        UpdateStates();
        UpdateMovement();
        UpdateAnims();
    }

    void UpdateInputs()
    {
        buttonA = Input.GetButton("ButtonA");
        buttonB = Input.GetButton("ButtonB");
        buttonX = Input.GetButton("ButtonX");
        buttonY = Input.GetButton("ButtonY");
    }

    void UpdateMovement()
    {
        if (!enableMovement)
            return;

        speed = Mathf.Abs(Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"));
        if (speed > 0f)
        {
            Quaternion newLookRotation = Quaternion.Euler(0, (Mathf.Atan2(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI) + 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, newLookRotation, lookSmooth);
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }

    void UpdateAnims()
    {
        anim.SetFloat("Speed", speed);
        if (buttonX && playerState == PlayerStates.CanPickUp)
        {
            anim.Play("PickUp");
            PickUpClosestObject();
        }
    }

    void UpdateStates()
    {
        if (currentResources.Count > 0)
        {
            playerState = PlayerStates.CanPickUp;
            FindObjectOfType<HUDControl>().SetPickUp(true);
        }
        else
        {
            playerState = PlayerStates.None;
            FindObjectOfType<HUDControl>().SetPickUp(false);
        }
    }

    void PickUpClosestObject()
    {
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in currentResources)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = t;
                minDist = dist;
            }
        }
        Item r = new Item();
        r.itemName = closest.GetComponent<Resource>().itemData.itemName;
        r.itemID = closest.GetComponent<Resource>().itemData.itemID;
        FindObjectOfType<Inventory>().AddItem(r);
        currentResources.Remove(closest);
        Destroy(closest.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Resource")
        {
            if (!currentResources.Contains(col.gameObject))
            {
                currentResources.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Resource")
        {
            if (currentResources.Contains(col.gameObject))
            {
                currentResources.Remove(col.gameObject);
            }
        }
    }

    public void SpawnLeftFootPrint()
    {
        GameObject clone = Instantiate(footPrintPrefab, leftFootPos.position, Quaternion.identity) as GameObject;
    }

    public void SpawnRightFootPrint()
    {
        GameObject clone = Instantiate(footPrintPrefab, rightFootPos.position, Quaternion.identity) as GameObject;
    }


}
