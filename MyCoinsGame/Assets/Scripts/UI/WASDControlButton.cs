/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // Ссылка на скрипт PlayerMovement

    public void SelectWASD()
    {
        playerMovement.controlType = PlayerMovement.ControlType.WASD; // Выбрано управление WASD
    }
}

*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement; 
    public GameObject settingsPanel; 

    public void SelectWASD()
    {
        settingsPanel.SetActive(true); 
        playerMovement.controlType = PlayerMovement.ControlType.WASD; 
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControlButton : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject settingsPanel;

    public void SelectWASD()
    {
        settingsPanel.SetActive(false);
        playerMovement.controlType = PlayerMovement.ControlType.WASD;
    }
}