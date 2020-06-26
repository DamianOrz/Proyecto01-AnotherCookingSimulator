using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VRTK;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PcManager : NetworkBehaviour
{
    private static int _idCombo;

    //PLAYER
    private Camera cameraPlayer;
    private GameObject canvasCrossHair;
    private RectTransform rectTransformCrossHair;
    private GameObject panelCrossHair;
    private Transform destinoHamburguesa;
    private Transform destination;
    private CharacterController controller;

    private GameObject canvasTomarPedidos;
    //MOVEMENT
    public float speed = 7f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    private Vector3 velocity;

    //Me fijo si toca piso
    private Transform groundCheck;
    private float groundDistance = 0.5f;
    public LayerMask groundMask;
    private bool isGrounded;

    //INTERACTION
    [Range(0.1f, 15f)]
    public float distanceToSee;
    RaycastHit whatIHit;
    private GameObject zonaVerificacionDisponible = null;

    //LOOK
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    //UI
    private GraphicRaycaster m_Raycaster;
    private EventSystem m_EventSystem;
    private PointerEventData m_PointerEventData;
    //PEDIDO
    [SerializeField] Pedido pedido = new Pedido();

    void Start()
    {
        //DiaManager.instanceDiaManager.EmpezarDia();
        controller = this.GetComponent<CharacterController>();
        cameraPlayer = this.GetComponentInChildren<Camera>();
        destination = cameraPlayer.transform.GetChild(0);
        groundCheck = this.transform.GetChild(2);

        canvasCrossHair = GameObject.Find("Canvas");
        rectTransformCrossHair = canvasCrossHair.GetComponent<RectTransform>();

        canvasTomarPedidos = GameObject.Find("CanvasTomarPedido");
        canvasTomarPedidos.GetComponent<Canvas>().worldCamera = cameraPlayer;

        //UI
        m_Raycaster = canvasTomarPedidos.GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        Cursor.lockState = CursorLockMode.Locked;
        destinoHamburguesa = GameObject.FindGameObjectWithTag("Verificacion").transform.GetChild(0).transform;
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
            canvasCrossHair.transform.GetChild(0).gameObject.SetActive(true);
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
        Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * distanceToSee, Color.magenta);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.name)
                {
                    case "btnHamburguesaSimple":
                        FindObjectOfType<AudioManager>().PlayInPosition("Tap", result.gameObject.transform.position);
                        SetIdCombo(1);
                        break;
                    case "btnHamburguesaDoble":
                        FindObjectOfType<AudioManager>().PlayInPosition("Tap", result.gameObject.transform.position);
                        SetIdCombo(2);
                        break;
                    case "btnHamburguesaSimpleConQueso":
                        FindObjectOfType<AudioManager>().PlayInPosition("Tap", result.gameObject.transform.position);
                        SetIdCombo(3);
                        break;
                    case "btnHamburguesaDobleConQueso":
                        FindObjectOfType<AudioManager>().PlayInPosition("Tap", result.gameObject.transform.position);
                        SetIdCombo(4);
                        break;
                }
                 Debug.Log("Hit " + result.gameObject.name);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out whatIHit, distanceToSee);
            if(destination.childCount > 0)
            {
                if(whatIHit.collider==null)
                {
                    DropObject(destination.GetChild(0).gameObject);
                    return;
                }
                if (whatIHit.collider.gameObject.tag == "Verificacion" && zonaVerificacionDisponible==null)
                {
                    string nombre = destination.GetChild(0).gameObject.name;
                    bool isBandeja = nombre.Contains("Bandeja");
                    if (isBandeja )
                    {
                        GameObject objetoAMover= destination.GetChild(0).gameObject;
                        zonaVerificacionDisponible = objetoAMover;
                        Vector3 scale = objetoAMover.transform.localScale;

                        objetoAMover.transform.position = destinoHamburguesa.position;
                        objetoAMover.transform.rotation = destinoHamburguesa.rotation;
                    }
                }
                DropObject(destination.GetChild(0).gameObject);        
            }
            else
            {
                if (whatIHit.collider.gameObject.tag == "Interactuable")
                {
                    if (whatIHit.collider.gameObject.name == "btnGenerarInterpretacion")
                    {
                        FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", whatIHit.collider.gameObject.transform.position);

                        PedidoManager.instancePedidoManager.CrearInterpretacion(_idCombo);

                        GameObject Boton = whatIHit.collider.gameObject;
                        Boton.GetComponent<Animation>().Play();

                        List<Pedido> pedidos = PedidoManager.instancePedidoManager.getListaPedidos();
                    }
                    if (whatIHit.collider.gameObject.name == "btnGenerarPedidoRandom")
                    {
                        FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", whatIHit.collider.gameObject.transform.position);

                        GameObject Boton = whatIHit.collider.gameObject;
                        Boton.GetComponent<Animation>().Play();
                        if (PedidoManager.instancePedidoManager.getListaPedidos().Count< DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia)
                        {
                            PedidoManager.instancePedidoManager.crearPedidoRandom();
                            List<Pedido> pedidos = PedidoManager.instancePedidoManager.getListaPedidos();
                        }   
                    }
                    if (whatIHit.collider.gameObject.name == "PantallaHacerPedidos")
                    {
                        //m_PointerEventData = new PointerEventData(m_EventSystem);
                        //GraphicRaycaster gr = canvasTomarPedidos.GetComponent<GraphicRaycaster>();
                        //PedidoManager.crearPedidoRandom(1);
                        //List<Pedido> pedidos = PedidoManager.getListaPedidos();
                    }
                }
                if (whatIHit.collider.gameObject.tag == "Grabable")
                {
                    if (!whatIHit.collider.gameObject.GetComponent<VRTK_InteractableObject>().IsInSnapDropZone())
                    {
                         PickUpObject(whatIHit.collider.gameObject);
                    }
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

        cameraPlayer.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void SetCrossHair()
    {
        rectTransformCrossHair = GetComponent<RectTransform>();
    }
    public static void SetIdCombo(int combo)
    {
        _idCombo = combo;
    }
    void PickUpObject(GameObject go)
    {
        Transform posicionObjeto = go.transform;
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.transform.position = destination.position;
        go.transform.rotation = destination.rotation;
        go.transform.parent = GameObject.Find("Destination").transform;
        if(zonaVerificacionDisponible!=null)
        {
            if (zonaVerificacionDisponible==go)
            {
                zonaVerificacionDisponible = null;
            }
        }
    }
    void DropObject(GameObject go)
    {
        //AGREGAR RAYCAST PARA APOYAR
        go.transform.parent = null;
        go.GetComponent<Rigidbody>().isKinematic = false;
    }
}