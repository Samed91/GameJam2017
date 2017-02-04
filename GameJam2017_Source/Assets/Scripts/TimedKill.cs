using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedKill : MonoBehaviour
{

    public float time = 1;

    void Start()
    {
        Invoke("Kill", time);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

}
