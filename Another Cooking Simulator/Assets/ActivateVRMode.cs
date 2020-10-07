using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVRMode : MonoBehaviour
{
    public GameObject canvas;
    public Object vrZonePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowVR()
    {
        Instantiate(vrZonePrefab);
    }

    public void DeleteGameobject()
    {
        GameObject a = GameObject.Find("MainMenuCanvas");
        a.gameObject.SetActive(true);
        GameObject b = this.transform.root.gameObject;
        Destroy(b);
    }
}
