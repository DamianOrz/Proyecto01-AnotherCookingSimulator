using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Transform camera;
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

    public GameObject Ball;
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
    [Client]
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
        Movement();
        //Interaction();
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


        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out whatIHit, distanceToSee))
        {
            if (Input.GetKeyDown(KeyCode.E) && whatIHit.collider.gameObject.tag == "BtnCrearPedidoRandom")
            {
                Debug.Log("Se crea el pedido random");
                FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", whatIHit.collider.gameObject.transform.position);
                PedidoManager.crearPedidoRandom(1);
                List<Pedido> pedidos = PedidoManager.getListaPedidos();
                Debug.Log("");
                GameObject Boton = whatIHit.collider.gameObject;
                Boton.GetComponent<Animation>().Play();
            }
        }
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out whatIHit, distanceToSee))
        {
            if (Input.GetKeyDown(KeyCode.E) && whatIHit.collider.gameObject.tag != "Interactable-object")
            {
                if (this.GetComponent<Inventory>().countOfBalls > 0)
                {
                    Instantiate(Ball, position, Ball.transform.rotation);
                    Debug.Log("I put a ball");
                    this.GetComponent<Inventory>().countOfBalls--;
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

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

}