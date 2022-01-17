using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnTutorialOnMouse_BikeMinigame2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public void OnPointerEnter(PointerEventData eventData)
    {
        GameController_BikeMinigame2.instance.isMouseOverButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameController_BikeMinigame2.instance.isMouseOverButton = false;
    }
}

