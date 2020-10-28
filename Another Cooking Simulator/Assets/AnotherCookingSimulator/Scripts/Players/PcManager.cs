using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VRTK;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;

public class PcManager : NetworkBehaviour
{
    #region DeclaracionVariables

    private static int _idCombo;

    //ONLINE
    public NetworkTransformChild ntc;

    //PLAYER
    private Camera cameraPlayer;
    private GameObject canvasCrosshair;
    private RectTransform rectTransformCrossHair;
    private RawImage myCrosshair;
    private GameObject panelCrossHair;
    private Transform destinoHamburguesa;
    private Transform destination;
    private CharacterController controller;
    Animator anim;

    private GameObject canvasSpawnIngrediente;
    private GameObject orderCreatorCanvas;
    private GameObject canvasTomarPedidos;
    private GameObject ordersMonitor;

    //MOVEMENT
    public float speed = 7f;
    public float sprintSpeed = 12;
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
    private GraphicRaycaster spawn_Raycaster;
    private GraphicRaycaster m_Raycaster;
    private EventSystem m_EventSystem;
    private PointerEventData m_PointerEventData;

    //Material
    private int idUltimoObjetoQueMiro;
    [SerializeField] private Material isSeeing;
    [SerializeField] private Material mesada;
    [SerializeField] private Material original;
    private Renderer rendDeLaMesa = null;
    //PEDIDO
    [SerializeField] Pedido pedido = new Pedido();

    #endregion

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
        gameObject.transform.GetChild(0).transform.rotation.Set(0, 177.436f, 0, 0);
        anim = this.GetComponent<Animator>(); //Se usa para las animaciones de caminar, saltar, etc.

        canvasCrosshair = GameObject.Find("Mira"); //Obtengo el gameobject que tiene mi mira
        rectTransformCrossHair = canvasCrosshair.GetComponent<RectTransform>();
        myCrosshair = canvasCrosshair.GetComponentInChildren<RawImage>();

        canvasSpawnIngrediente = GameObject.Find("CanvasSpawnIngrediente");
        canvasSpawnIngrediente.GetComponent<Canvas>().worldCamera = cameraPlayer;
        spawn_Raycaster = canvasSpawnIngrediente.GetComponent<GraphicRaycaster>();


        orderCreatorCanvas = GameObject.Find("OrderCreatorCanvas");
        orderCreatorCanvas.GetComponent<Canvas>().worldCamera = cameraPlayer;
        //ordersMonitor = GameObject.Find("OrdersMonitorCanvas");
        //ordersMonitor.GetComponent<Canvas>().worldCamera = cameraPlayer;
        //UI
        m_Raycaster = orderCreatorCanvas.GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        Cursor.lockState = CursorLockMode.Locked;
        destinoHamburguesa = GameObject.FindGameObjectWithTag("Verificacion").transform.GetChild(0).transform;
    }
    // Update is called once per frame
    void Update()
    {
            if (!transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority)
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
            Movement();
            if (!PauseManager.isCanvasBeingUsed() && !DiaManager.instanceDiaManager.isCanvasBeingUsed())
            {
                SetCrossHair();
                Interaction();
                Look();
            }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else { speed = 7; }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //isGrounded = Physics.Raycast(transform.position, -Vector3.up, (float)(this.transform.position.y) + 0.1f);
        //if (isGrounded == true && transform.position.y >= 7)
        //{
        //    isGrounded = false;
        //}

        bool isWalking = false;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move != new Vector3(0, 0, 0))
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
            Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out whatIHit, distanceToSee);
            if ( whatIHit.collider == null) return;
            if (whatIHit.collider.gameObject.tag != "Interactuable") return;
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out whatIHit, distanceToSee);
            //Pregunto si ya tiene algo agarrado
            if (destination.childCount > 0)
            {
                if (whatIHit.collider == null)
                {
                    CmdDropObject(gameObject);
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
                if (whatIHit.collider.gameObject.tag == "Mesa" || whatIHit.collider.gameObject.tag == "Mesada")
                {
                    if(whatIHit.collider.gameObject.tag == "Mesa")
                    {
                        int iMesaId = whatIHit.collider.gameObject.GetComponent<MesaIdentifier>().id;
                        //PutObjectOnTheTable(gameObject,whatIHit);
                        if(destination.GetChild(0).gameObject.name.Contains("Bandeja"))
                        {
                            PedidoManager.instancePedidoManager.EvaluarPedido(iMesaId, destination.GetChild(0).gameObject);
                            Debug.Log("[LOCAL][DAMIAN] La mesa en la que se dejó el pedido es: " + iMesaId); 
                        }
                    }
                    
                    CmdPutObjectOnTheTable(gameObject, whatIHit.point);
                    return;
                }
                CmdDropObject(gameObject);
                return;
            }
            if (whatIHit.collider == null) return;
            //Pregunta si apunta a un objeto interactuable
            if (whatIHit.collider.gameObject.tag == "Interactuable")
            {
            }
            //Pregunto si esta apuntando a un objeto que se puede agarrar
            if (whatIHit.collider.gameObject.tag == "Grabable")
            {
                if (!whatIHit.collider.gameObject.GetComponent<VRTK_InteractableObject>().IsInSnapDropZone())
                {
                    CmdPickUpObject(whatIHit.collider.gameObject, gameObject);
                    //PonerNetworkTransformChild(whatIHit.collider.gameObject);
                }
            }
        }
    }
    #region ONLINE_PickUpObject
    [Command]
    void CmdPickUpObject(GameObject go, GameObject player)
    {
        Debug.Log("This is being executed in the server");
        RpcSetAsChild(go, player);
    }

    [ClientRpc]
    void RpcSetAsChild(GameObject childObject, GameObject player)
    {
        childObject.SetActive(false);
        Debug.Log("SIMON :" + transform.name);
        childObject.transform.parent = player.transform.GetChild(1).transform.GetChild(0).transform;
        childObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        childObject.GetComponent<Collider>().enabled = false;
        childObject.GetComponent<Rigidbody>().isKinematic = true;
        //childObject.transform.position = new Vector3(0, 0, 0);
        if (zonaVerificacionDisponible != null)
        {
            if (zonaVerificacionDisponible == childObject)
            {
                zonaVerificacionDisponible = null;
            }
        }
        childObject.SetActive(true);
        //childObject.transform.rotation = localRot;
    }
    #endregion

    #region ONLINE_DropObject
    [Command]
    void CmdDropObject(GameObject player)
    {
        Debug.Log("This is being executed in the server");
        RpcRemoveAsChild(player);
    }
    [Command]
    void CmdPutObjectOnTheTable(GameObject player, Vector3 point)
    {
        RpcPutObjectOnTheTable(player, point);

        //Se debe hacer verificacion del pedido para saber si se entregó en la mesa correcta:
        //FALTA HACER EL CODIGO
    }
    [ClientRpc]
    void RpcPutObjectOnTheTable(GameObject player, Vector3 point)
    {
        Debug.Log("SIMON: EL PUNTO ES : " + point);
        GameObject objetoAgarrado = player.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
        Debug.Log("SIMON: EL INGREDIENTE ES : " + objetoAgarrado.gameObject.name);
        objetoAgarrado.SetActive(false);
        objetoAgarrado.transform.parent = null;
        objetoAgarrado.GetComponent<Collider>().enabled = false;
        objetoAgarrado.GetComponent<Rigidbody>().isKinematic = true;
        objetoAgarrado.transform.position = new Vector3(point.x, point.y + 0.1f, point.z);
        objetoAgarrado.transform.rotation = new Quaternion(0, 0, 0, 0);
        objetoAgarrado.SetActive(true);
        AudioSource a = objetoAgarrado.GetComponent<AudioSource>();
        a.Play();
        StartCoroutine(WaitForSound(a, a.clip, objetoAgarrado));
    }


    public IEnumerator WaitForSound(AudioSource a, AudioClip Sound, GameObject bandeja)
    {
        yield return new WaitUntil(() => a.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        Destroy(bandeja);
    }

    IEnumerator WaitForSeconds(float time)
    {
        //NO FUNCIONA, REVISAR
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);
    }


    [ClientRpc]
    void RpcRemoveAsChild(GameObject player)
    {
        GameObject destinationDelPlayer = player.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
        
        destinationDelPlayer.SetActive(false);
        Debug.Log("SIMON :" + transform.name);
        destinationDelPlayer.transform.parent = null;
        destinationDelPlayer.GetComponent<Collider>().enabled = true;
        destinationDelPlayer.GetComponent<Rigidbody>().isKinematic = false;

        destinationDelPlayer.SetActive(true);
    }
    #endregion
    private void PutObjectOnTheTable(GameObject player, RaycastHit whatIhit)
    {
        Vector3 point = whatIHit.point;
        GameObject destinationDelPlayer = player.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
        destinationDelPlayer.SetActive(false);
        destinationDelPlayer.transform.parent = null;
        destinationDelPlayer.GetComponent<Rigidbody>().isKinematic = false;
        destinationDelPlayer.GetComponent<Collider>().enabled = true;
        destinationDelPlayer.transform.position = new Vector3(point.x, point.y + 0.01f, point.z);
        destinationDelPlayer.SetActive(true);
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
        if (whatIHit.collider == null)
        {
            if (rendDeLaMesa != null)
            {
                if (idUltimoObjetoQueMiro == 0) rendDeLaMesa.sharedMaterial = original;
                if (idUltimoObjetoQueMiro == 1) rendDeLaMesa.sharedMaterial = mesada;
            }
            return;
        }
        else if (whatIHit.collider != null && (whatIHit.collider.gameObject.tag == "Interactuable") && (whatIHit.collider.gameObject.name.Contains("PantallaHacerPedidos") || whatIHit.collider.gameObject.name.Contains("PantallaSpawnearIngredientes")) || whatIHit.collider.gameObject.name.Contains("CajaRegistradora"))
        {
            SetTabletCrosshair(); //Crosshair para cuando se usan las laptops
        }
        else
        {
            SetCrossHair(); //Crosshair básico/default
        }
        IluminarMesa();
    }
    private void IluminarMesa()
    {
        if ((whatIHit.collider.gameObject.tag == "Mesa" || whatIHit.collider.gameObject.tag == "Mesada") && destination.childCount > 0)
        {
            rendDeLaMesa = whatIHit.collider.gameObject.GetComponent<Renderer>();
            rendDeLaMesa.sharedMaterial = isSeeing;
            if (whatIHit.collider.gameObject.tag == "Mesa") idUltimoObjetoQueMiro = 0;
            if (whatIHit.collider.gameObject.tag == "Mesada") idUltimoObjetoQueMiro = 1;
        }
        else
        {
            if (rendDeLaMesa != null)
            {
                if(idUltimoObjetoQueMiro == 0) rendDeLaMesa.sharedMaterial = original;
                if (idUltimoObjetoQueMiro == 1) rendDeLaMesa.sharedMaterial = mesada;
            }
        }
    }
    void SetTabletCrosshair()
    {
        //rectTransformCrossHair = GetComponent<RectTransform>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        myCrosshair.enabled = false;


    } //Crosshair para cuando se usan las laptops

    void SetCrossHair()
    {
        //rectTransformCrossHair = GetComponent<RectTransform>()
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        myCrosshair.enabled = true;
    }  //Crosshair básico/default

    public static void SetIdCombo(int combo)
    {
        _idCombo = combo;
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