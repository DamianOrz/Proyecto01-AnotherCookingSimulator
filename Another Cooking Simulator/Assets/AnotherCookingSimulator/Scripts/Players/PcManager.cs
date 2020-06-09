﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PcManager : NetworkBehaviour
{
    private static int _idCombo;

    private RectTransform crossHair;

    public CharacterController controller;
    public Camera camera;
    //MOVEMENT
    public float speed = 7f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    //Me fijo si toca piso
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //INTERACTION
    [Range(0.1f, 15f)]
    public float distanceToSee;
    RaycastHit whatIHit;

    Vector3 position = new Vector3(-6.13f, 1.937f, -5.388f);

    GameObject player;

    [SerializeField] Pedido pedido = new Pedido();

    //LOOK
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    void start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if (!base.hasAuthority)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<AudioListener>().enabled = true;
        }
        SetCrossHair();
        Movement();
        Interaction();
        Look();
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    void Interaction()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * distanceToSee, Color.magenta);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(camera.transform.position, camera.transform.forward, out whatIHit, distanceToSee);
            if (whatIHit.collider.gameObject.tag == "Interactuable")
            {
                if (whatIHit.collider.gameObject.name=="btnGenerarPedido")
                {
                    FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", whatIHit.collider.gameObject.transform.position);

                    GameObject Boton = whatIHit.collider.gameObject;
                    Boton.GetComponent<Animation>().Play();
                    
                     
                    _idCombo = Random.Range(1, 4);
                    PedidoManager.crearPedidoRandom(1);
                    PedidoManager.CrearInterpretacion(_idCombo);
                    List<Pedido> pedidos = PedidoManager.getListaPedidos();
                }
            }
            if (whatIHit.collider.gameObject.tag == "Interactuable")
            {
                if (whatIHit.collider.gameObject.name == "btnHamburguesaSimple")
                {
                    Debug.Log("Crear Pedido");
                    PedidoManager.crearPedidoRandom(1);
                    List<Pedido> pedidos = PedidoManager.getListaPedidos();
                    GameObject Boton = whatIHit.collider.gameObject;
                    Boton.GetComponent<Animation>().Play();
                }
            }
        }
    }
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    void SetCrossHair()
    {
        crossHair = GetComponent<RectTransform>();
    }
    public static void SetIdCombo(int combo)
    {
        _idCombo = combo;
    }
}