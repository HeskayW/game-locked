using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float lookSpeed;
    public float lookCD;
    public float inputDelay;

    public float mode = 1f; // modo 1 es mover | modo 2 es apuntar
    

    public GameObject gun;


    private Vector3 dir;
    private float dirX;
    private float dirZ;
    private float angle;
    private float prevAngle;
    public float angleTimer;
    public float lastInputRegistered;
        private CharacterController cc;


    void Start()
    {
        mode = 1;
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (Input.GetAxis("Move") != 0 || Input.GetAxis("Aim") != 0)
            ChangeMode();

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            dirX = Input.GetAxis("Horizontal");
            dirZ = Input.GetAxis("Vertical");
        }
        else
        {
            dirX = 0f;
            dirZ = 0f;
        }

    }

    public void ChangeMoveSpeed(float var)
    {
        moveSpeed = var;
    }
    public void ChangeRotateCD(float var)
    {
        lookCD = var;
    }

    private void FixedUpdate()
    {
        ChooseAction();
    }

    private void ChooseAction()
    {
        if (mode == 1)
            MoveCharacter();
        else if (mode == 2)
            MoveGun();
    }

    private void ChangeMode()
    {
        if (Input.GetAxis("Move") != 0)
            mode = 1;
        else
            mode = 2;

        PlayerLights.ins.ToggleLight(mode);
    }

    private void MoveCharacter()
    {
        dir = new Vector3(dirX, 0f, dirZ).normalized;
        Vector3 mov = dir * Time.deltaTime * moveSpeed;

        //Debug.Log(mov.magnitude);

        cc.Move(mov);

    }

    private void MoveGun()
    {
        angleTimer += Time.deltaTime;

        lastInputRegistered += Time.deltaTime;

        if (dirX == 0 && dirZ == 0)
            return;

        if (angleTimer < lookCD)
            return;

        dir = new Vector3(dirX, 0f, -dirZ).normalized;

        if (dir.x > 0 && dirZ == 0)
        {
            angle = 0f;
        }
        else if (dir.x < 0 && dir.z == 0)
        {
            angle = 180f;
        }
        else if (dir.x == 0 && dir.z > 0)
        {
            angle = 90f;
        }
        else if (dir.x == 0 && dir.z < 0)
        {
            angle = 270f;
        }
        else if (dir.x > 0 && dir.z > 0)
        {
            angle = 45f;
            lastInputRegistered = 1;
        }
        else if (dir.x < 0 && dir.z > 0)
        {
            angle = 135f;
            lastInputRegistered = 1f;
        }
        else if (dir.x > 0 && dir.z < 0)
        {
            angle = 315f;
            lastInputRegistered = 1f;
        }
        else if (dir.x < 0 && dir.z < 0)
        {
            angle = 225f;
            lastInputRegistered = 1f;
        }

        if (lastInputRegistered < inputDelay)
            return;


        if (prevAngle != angle)
        {
            gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            prevAngle = angle;
            angleTimer = 0f;
        } 


        //gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, Quaternion.AngleAxis(angle, Vector3.up),45f);


    }

}
