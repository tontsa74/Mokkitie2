using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color defaultColor;

    private void Start()
    {
        defaultColor = GetComponent<Image>().color;
    }
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        GetComponent<Image>().color = Color.black;
        //GetComponentInChildren<TextMeshProUGUI>().rectTransform.rotation = Quaternion.Euler(0, 0, 10);

        GetComponentInChildren<RectTransform>().rotation = Quaternion.Euler(0, 0, 10);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        GetComponent<Image>().color = defaultColor;

       // GetComponentInChildren<TextMeshProUGUI>().rectTransform.rotation = Quaternion.Euler(0, 0, 0);

         GetComponentInChildren<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);


    }
}