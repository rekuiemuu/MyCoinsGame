using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public GameObject panel;

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

