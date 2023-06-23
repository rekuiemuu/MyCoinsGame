using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // ������ �� ������ PlayerMovement, ���������� �� �������� ���������
    public GameObject settingsPanel;

    public void SelectBothControl()
    {
        settingsPanel.SetActive(false);
        playerMovement.controlType = PlayerMovement.ControlType.Both; // ���������� ���������� �� ���������� � �����, � WASD
    }
}
