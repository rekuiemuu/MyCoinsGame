using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    public GameObject panel;

    public void nextgame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}