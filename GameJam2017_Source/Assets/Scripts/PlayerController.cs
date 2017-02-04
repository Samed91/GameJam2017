using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(0, 5)]
    public float movementSpeed = 3f;
    [Range(0, 5)]
    public float lookSpeed = 3f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * (lookSpeed * Time.deltaTime);
        float v = Input.GetAxis("Vertical") * (lookSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(h, v) * Mathf.Rad2Deg, transform.eulerAngles.z);
        transform.Translate(transform.forward * (movementSpeed * Time.deltaTime));

    }

}
