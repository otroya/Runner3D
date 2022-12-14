using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpHeigh;

    Vector3 m_Movement = Vector3.zero;
    private Rigidbody m_Rigidbody;

    InputControler ic;

    float m_RotationSpeed = 200;
    int saltos = 2;
    bool suspendido = false;

    Animator animator;

    [SerializeField] GameObject spawnpoint;
    [SerializeField] List<GameObject> teletransport;

    [SerializeField] private GameEvent onEarn;
    [SerializeField] private GameEvent onReset;

    GameObject gob;
    [SerializeField] private GameObject[] spawns;

    public int spawn = 0;

    private void Awake()
    {
        ic = new InputControler();
        ic.InFloor.Jump.started += saltar;
        ic.InFloor.Enable();
        m_Rigidbody = GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }

    private void saltar(InputAction.CallbackContext obj)
    {
        if (saltos > 0)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpHeigh * 2, ForceMode.Impulse);
            saltos--;
        }
    }

    void Update()
    {
        Vector2 input = ic.InFloor.Movement.ReadValue<Vector2>();
        m_Movement = Vector3.zero;
        //Vector3 inputSalt = ic.InFloor.Jump.ReadValue<Vector3>();
        if (input.y > 0)
        {
            if (!suspendido)
                animator.Play("Walk With Briefcase");

            m_Movement += transform.forward;
            if (input.x < 0)
                transform.Rotate(-Vector3.up * m_RotationSpeed * Time.deltaTime);
            if (input.x > 0)
                transform.Rotate(Vector3.up * m_RotationSpeed * Time.deltaTime);
        }
        else if (input.y < 0)
        {
            if (!suspendido)
                animator.Play("Walk With Briefcase");
            m_Movement -= transform.forward;
            if (input.x < 0)
                transform.Rotate(Vector3.up * m_RotationSpeed * Time.deltaTime);
            if (input.x > 0)
                transform.Rotate(-Vector3.up * m_RotationSpeed * Time.deltaTime);
        }
        else
        {
            if (input.x < 0)
            {
                m_Movement -= transform.right;
                if (!suspendido)
                    animator.Play("Walk Strafe Left");
            }
            if (input.x > 0)
            {
                m_Movement += transform.right;
                if (!suspendido)
                    animator.Play("Walk Strafe Right");
            }
        }

        m_Movement.Normalize();

        if (suspendido)
        {
            animator.Play("Jumping");
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.MovePosition(transform.position + m_Movement * m_Speed * Time.fixedDeltaTime);
        m_Rigidbody.AddForce(Physics.gravity * 5, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == spawns[0])
        {
            spawn = 0;
        }
        else if (collision.gameObject == spawns[1])
        {
            spawn = 1;
        }

        if (gob!=null)
            if (gob != collision.gameObject)
            {
                onEarn.Raise();
            }
            else
            {

            }
        gob = collision.gameObject;

        if (collision.gameObject.tag == "suelo" || collision.gameObject.tag == "spawnpoint" || collision.gameObject.tag == "tp")
        {
            saltos = 2;
            suspendido = false;
        }

        if (collision.gameObject.tag == "kill" || collision.gameObject.tag == "lava")
        {
            this.transform.position = spawnpoint.transform.position + spawnpoint.transform.up * 2;
            print("kill");
            onReset.Raise();
        }
        else if (collision.gameObject.tag == "tp")
        {
            this.transform.position = teletransport[0].transform.position;
            print("tp");
        }
        else if (collision.gameObject.tag == "spawnpoint")
        {
            spawnpoint = collision.gameObject;
            print("spawnpoint");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "suelo")
        {
            suspendido = true;
        }
    }

}
