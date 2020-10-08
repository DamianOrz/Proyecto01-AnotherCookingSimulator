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
        GameObject vrZone = GameObject.Find("VRPrefab(Clone)");

        vrZone.GetComponent<ActivateVRMode>().setCanvas(this.transform.root.gameObject);
    }

    public void setCanvas(GameObject newCanvas)
    {
        canvas = newCanvas;
    }

    public void DeleteGameobject()
    {
        GameObject a = GameObject.Find("MainMenuCanvas");
        a.gameObject.SetActive(true);
        GameObject b = this.transform.root.gameObject;
        Destroy(b);
    }
}
