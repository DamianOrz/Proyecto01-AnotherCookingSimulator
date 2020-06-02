using UnityEngine;
using UnityEngine.UI;
using VRTK.Controllables;


public class ButtonInteraction : MonoBehaviour
{
    public VRTK_BaseControllable controllable;
    public Text displayText;
    [SerializeField] public GameObject outputOnMax;
    public string outputOnMin = "Minimum Reached";

    protected virtual void OnEnable()
    {
        controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
        controllable.ValueChanged += ValueChanged;
        controllable.MaxLimitReached += MaxLimitReached;
        controllable.MinLimitReached += MinLimitReached;
    }

    protected virtual void ValueChanged(object sender, ControllableEventArgs e)
    {
        if (displayText != null)
        {
            displayText.text = e.value.ToString("F1");
        }
    }

    protected virtual void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", this.gameObject.transform.position);
        Vector3 position = new Vector3(-3f, 1.5f, -2.652f);
        Instantiate(outputOnMax,position,transform.rotation);
    }

    protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
    {
        if (outputOnMin != "")
        {
            Debug.Log(outputOnMin);
        }
    }
}