using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates {None, CanPickUp,CanInteract,CanTalk}

public class PlayerController : MonoBehaviour
{
    public PlayerStates playerState = PlayerStates.None;
    public Animator anim;
    [Range(0, 5)]
    public float movementSpeed = 3f;
    [Range(0, 5)]
    public float lookSmooth = 3f;
    private float speed;
    public bool enableMovement = false;
    private bool buttonA = false;
    private bool buttonB = false;
    private bool buttonX = false;
    private bool buttonY = false;

    void Update()
    {
        UpdateInputs();
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
        if (speed > 0.1f)
        {
            Quaternion newLookRotation = Quaternion.Euler(0, (Mathf.Atan2(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI) + 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, newLookRotation, lookSmooth);
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }

    void UpdateAnims()
    {
        anim.SetFloat("Speed", speed);
        if (buttonX)
            anim.Play("PickUp");
    }

}
