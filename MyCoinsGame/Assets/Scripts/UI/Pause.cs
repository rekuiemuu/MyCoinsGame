using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    public void pause()
    { panel.SetActive(true);
        Time.timeScale = 0;
    }
}
