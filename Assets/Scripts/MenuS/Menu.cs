using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Menu : MonoBehaviour
{
    public void PLAY()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EXIT()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
