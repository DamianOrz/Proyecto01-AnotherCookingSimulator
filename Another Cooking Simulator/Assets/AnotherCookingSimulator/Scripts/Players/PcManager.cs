using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VRTK;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class PcManager : NetworkBehaviour
{
    private static int _idCombo;

    //ONLINE
    public NetworkTransformChild ntc;

    //PLAYER
    private Camera cameraPlayer;
    private GameObject canvasCrosshair;
    private RectTransform rectTransformCrossHair;
    private Image[] myCrosshair;
    private GameObject panelCrossHair;
    private Transform destinoHamburguesa;
    private Transform destination;
    private CharacterController controller;
    Animator anim;

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
        if (!transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }

        FindObjectOfType<AudioManager>().SwapLobbyMusicToGameMusic("LobbyMusic", "GameMusic");

        controller = this.GetComponent<CharacterController>();
        cameraPlayer = this.GetComponentInChildren<Camera>();
        destination = cameraPlayer.transform.GetChild(0);
        groundCheck = this.transform.GetChild(2);

        anim = this.GetComponent<Animator>(); //Se usa para las animaciones de caminar, saltar, etc.

        canvasCrosshair = GameObject.Find("Mira"); //Obtengo el gameobject que tiene mi mira
        rectTransformCrossHair = canvasCrosshair.GetComponent<RectTransform>();
        myCrosshair = canvasCrosshair.GetComponentsInChildren<Image>();


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
        if (!transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority) //Si no tiene autoridad se desactiva? Revisar
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }
        else
        {
            canvasCrosshair.transform.GetChild(0).gameObject.SetActive(true);
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

        bool isWalking = false;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        

        Vector3 move = transform.right * x + transform.forward * z;

        if (move != new Vector3(0,0,0))
        {
            isWalking = true;
        }
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsGrounded", isGrounded);

        controller.Move(move * speed * Time.deltaTime);
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        anim.SetFloat("Speed", -(velocity.y)); //Por ahora siempre es 6/-6
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
                        FindObjectOfType<AudioManager>().Play("FX-Tap");
                        SetIdCombo(1);
                        break;
                    case "btnHamburguesaDoble":
                        FindObjectOfType<AudioManager>().Play("FX-Tap");
                        SetIdCombo(2);
                        break;
                    case "btnHamburguesaSimpleConQueso":
                        FindObjectOfType<AudioManager>().Play("FX-Tap");
                        SetIdCombo(3);
                        break;
                    case "btnHamburguesaDobleConQueso":
                        FindObjectOfType<AudioManager>().Play("FX-Tap");
                        SetIdCombo(4);
                        break;
                }
                Debug.Log("Hit " + result.gameObject.name);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out whatIHit, distanceToSee);
            //Pregunto si ya tiene algo agarrado
            if (destination.childCount > 0)
            {
                if (whatIHit.collider == null)
                {
                    DropObject(destination.GetChild(0).gameObject);
                    return;
                }
                if (whatIHit.collider.gameObject.tag == "Verificacion" && zonaVerificacionDisponible == null)
                {
                    string nombre = destination.GetChild(0).gameObject.name;
                    bool isBandeja = nombre.Contains("Bandeja");
                    if (isBandeja)
                    {
                        GameObject objetoAMover = destination.GetChild(0).gameObject;
                        zonaVerificacionDisponible = objetoAMover;
                        Vector3 scale = objetoAMover.transform.localScale;

                        objetoAMover.transform.position = destinoHamburguesa.position;
                        objetoAMover.transform.rotation = destinoHamburguesa.rotation;
                    }
                }
                DropObject(destination.GetChild(0).gameObject);
                return;
            }
            //Pregunta si apunta a un objeto interactuable
            if (whatIHit.collider.gameObject.tag == "Interactuable")
            {
                if (whatIHit.collider.gameObject.name == "btnGenerarInterpretacion")
                {
                    FindObjectOfType<AudioManager>().Play("FX-ButtonClick");

                    PedidoManager.instancePedidoManager.CrearInterpretacion(_idCombo);

                    GameObject Boton = whatIHit.collider.gameObject;
                    Boton.GetComponent<Animation>().Play();

                    List<Pedido> pedidos = PedidoManager.instancePedidoManager.getListaPedidos();
                }
                if (whatIHit.collider.gameObject.name == "btnGenerarPedidoRandom")
                {
                    FindObjectOfType<AudioManager>().Play("FX-ButtonClick");

                    GameObject Boton = whatIHit.collider.gameObject;
                    Boton.GetComponent<Animation>().Play();
                    if (PedidoManager.instancePedidoManager.getListaPedidos().Count < DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia)
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
            //Pregunto si esta apuntando a un objeto que se puede agarrar
            if (whatIHit.collider.gameObject.tag == "Grabable")
            {
                if (!whatIHit.collider.gameObject.GetComponent<VRTK_InteractableObject>().IsInSnapDropZone())
                {
                    CmdPickUpObject(whatIHit.collider.gameObject,destination.gameObject);
                    //PonerNetworkTransformChild(whatIHit.collider.gameObject);
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

        Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out whatIHit, distanceToSee);
        if (whatIHit.collider != null && whatIHit.collider.gameObject.name.Contains("PantallaHacerPedidos"))
        {
            SetTabletCrosshair(); //Crosshair para cuando se usan las laptops
        }
        else
        {
            SetCrossHair(); //Crosshair básico/default
        }
    }

    void SetTabletCrosshair()
    {
        //rectTransformCrossHair = GetComponent<RectTransform>();
        Cursor.lockState = CursorLockMode.Locked;
        foreach (Image i in myCrosshair) //Son varias partes del crosshair separadas
        {
            i.color = Color.red;
        }
        
    } //Crosshair para cuando se usan las laptops

    void SetCrossHair()
    {
        //rectTransformCrossHair = GetComponent<RectTransform>()
        foreach (Image i in myCrosshair) //Son varias partes del crosshair separadas
        {
            i.color = new Color(5, 255, 0); //Verde --> RGB
        };
    }  //Crosshair básico/default

    public static void SetIdCombo(int combo)
    {
        _idCombo = combo;
    }

    [Command]
    void CmdPickUpObject(GameObject go,GameObject place)
    {
        Debug.Log("This is being executed in the server");
        RpcSetAsChild(go,place);
    }
    [ClientRpc]
    void RpcSetAsChild(GameObject childObject,GameObject place)
    {
        childObject.SetActive(false);
        childObject.transform.parent = this.destination;
        childObject.GetComponent<Rigidbody>().isKinematic = true;
        //childObject.transform.position = new Vector3(0, 0, 0);
        childObject.SetActive(true);
        //childObject.transform.rotation = localRot;
    }



    [ClientRpc]
    void RpcPickUpInEachClient(GameObject go)
    {
        Debug.Log("SIMON : AVISANDO QUE ALGUIEN AGARRO ALGO");
        Transform posicionObjeto = go.transform;
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.transform.position = destination.position;
        go.transform.rotation = destination.rotation;
        go.transform.parent = GameObject.Find("Destination").transform;

        if(zonaVerificacionDisponible != null)
        {
            if (zonaVerificacionDisponible == go)
            {
                zonaVerificacionDisponible = null;
            }
        }
        Debug.Log("SIMON : SE TERMINO EL AVISO");
    }

    void DropObject(GameObject go)
    {
        //AGREGAR RAYCAST PARA APOYAR
        go.transform.parent = null;
        go.GetComponent<Rigidbody>().isKinematic = false;
    }

    void PonerNetworkTransformChild(GameObject go)
    {
        ntc.target = go.GetComponent<Transform>();
        ntc.setClieltAuthority();
    }
    void QuitarNetworkTransformChild()
    {

    }
}