using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // ������ �� ������ PlayerMovement, ���������� �� �������� ���������
    public GameObject settingsPanel;

    public void SelectMouseControl()
    {
        settingsPanel.SetActive(false);
        playerMovement.controlType = PlayerMovement.ControlType.Mouse; // ���������� ���������� �� ���������� �����
    }
}
