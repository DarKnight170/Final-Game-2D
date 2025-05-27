using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour
{
    public GameObject MenuPausa;
    public bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            if (juegoPausado)
            {
                REANUDAR();
            }
            else
            {
                PAUSAR();
            }
        }

    }

    public void REANUDAR()
    {
        MenuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    public void PAUSAR()
    {
        MenuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado=true;
    }
    
    public void SALIRMENU()
    {
        SceneManager.LoadScene("Menu");
    }

}