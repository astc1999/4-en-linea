using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Grid2D()
    {
        SceneManager.LoadScene(1); // carga la escena 1 que es el juego
    }
    public  void inicio()
    {
        SceneManager.LoadScene(0);// carga la escena 0 que es el menu
    }
    public  void terminarazul()
    {
        SceneManager.LoadScene(2);// carga la escena 2 que es cuando el jugador azul gano
    }
    public  void terminarrojo()
    {
        SceneManager.LoadScene(3);// caraga la escena 3 que es cuando el jugador rojo gano
    }

    public void salir()
    {
        Application.Quit();// detiene las acciones
    }
  
 
    
}
