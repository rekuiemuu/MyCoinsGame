using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // —сылка на скрипт PlayerMovement, отвечающий за движение персонажа
    public GameObject settingsPanel;

    public void SelectBothControl()
    {
        settingsPanel.SetActive(false);
        playerMovement.controlType = PlayerMovement.ControlType.Both; // ”становить управление на управление и мышью, и WASD
    }
}
