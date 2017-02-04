using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    public GameObject pickUpPrompt;

    public void SetPickUp(bool b)
    {
        pickUpPrompt.SetActive(b);
    }

}
