using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // —сылка на скрипт PlayerMovement, отвечающий за движение персонажа
    public GameObject settingsPanel;

    public void SelectMouseControl()
    {
        settingsPanel.SetActive(false);
        playerMovement.controlType = PlayerMovement.ControlType.Mouse; // ”становить управление на управление мышью
    }
}
