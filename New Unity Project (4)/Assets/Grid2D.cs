
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid2D : MonoBehaviour  // nombre de la clase
{
    private bool primerturno;    // variable booleana turno de jugador
  
    private GameObject[,] grid;  // variable privada de un objeto esfera
    private int height=10;       // variable publica de altura
    private int width=10;        // variable publica de ancho
    bool ganador;                // variable booleana 
    public Text contador;         // variable publica texto del contador
    public Text fin;              // variable publica texto que se ejecuta cuando termine el contador
    private float tiempo =55f;    // variable de tiempo asignado
    

 
  
 
         

    void Start()
    {
                               
        fin.enabled=false;  // variable falsa que detiene el ciclo


        grid = new GameObject[width, height];     //ubicacion del objeto para ancho y alto
        for (int i = 0; i < width; i++)       //bucle para crear las esferas a lo ancho
        {
            
            for (int j = 0; j < height; j++)      //bucle para crear las esferas a lo alto
            { 
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);    // se crea un objeto tipo esfera con el createprimitive
                go.transform.position= new Vector3(i,j,0);     //la esfera almacenada se ubica en una posicion en el vector 3
                grid[i,j]=go;       // coordenadas del objeto en x ,y

                go.GetComponent<Renderer>().material.color = Color.white;   //se  establece un color inicial para el objeto

                grid[i, j] = go;    //el objeto grid es igual al a la variable go
            }
        }
    }

    void Update()
    {
        tiempo -=Time.deltaTime; // llamado a la funcion tiempo de forma descendente
        contador.text = "" +tiempo.ToString("f0");// muestra el temporizador de tiempo en pantalla
      
        if(tiempo<=0 )  // bucle condicionador de tiempo
        {
        
            
            fin.enabled= true;  // si tiempo es igual a 0 la variable es verdadera se ejecuta esta accion
            SceneManager.LoadScene(1);  // si tiempo es igual a 0 y no hay ganador se reinicia la escena 

        }
         
        
        

        Vector3 mPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);   // la camara ubica la posicion del mouse en vector 3 x, y
        

        if (Input.GetKey(KeyCode.Mouse0)  && ganador==false )  // bucle que al dar clic genera una accion y si ganador es falso detiene el ciclo
        {
            UpdatePickedPiece(mPosition);    // al colocarme encima de una pieza o en el caso mio que son esferas muestre la posicion
        }
    }

    void UpdatePickedPiece(Vector3 position) // clase que le da parametros al objeto o a la pieza  en x,y
    {
        int i = (int)(position.x + 0.5f);        //variable i se ubica en una pocicion x
        int j = (int)(position.y + 0.5f);        //variable j se ubica en una pocicion y

        if (Input.GetButtonDown("Fire1")) //bucle que me permite dar clic sobre el objeto y este genere una accion
        {
            if (i >= 0 && j >= 0 && i < width && j< height)   //variable i y variable j se ubican alo ancho y a lo alto
            {
                GameObject go=grid[i,j];     //el objeto se pone en el espacio i y en el j
                if (go.GetComponent<Renderer>().material.color == Color.white)  //se renderisa el color inicial del fondo
                {
                    Color colorAUsar = Color.clear; // color base o estandar
                    if (primerturno)                // bucle condicionador de turno jugador 1
                    colorAUsar = Color.blue;       //el color del primer turno es azul


                    else                        
                    colorAUsar = Color.red;  // color a usar del siguiente turno es rojo

                   
                    

                    go.GetComponent<Renderer>().material.color = colorAUsar; //se establece que el color a usar por turno es diferente para el objeto
                    primerturno = !primerturno; 
                    VerificadorX(i, j, colorAUsar); //verificador de color en horizontal 
                    VerificadorY(i, j, colorAUsar); //verificador de color en vertical 
                    DiagoPositiva(i, j, colorAUsar); //verificador de color en diagonal positiva
                    DiagoNegativa(i, j, colorAUsar); // verificador de color en diagonal negativa
                }
            }
        }
    }
    public void VerificadorX(int x, int y, Color colorVerificar) //clase verificadora de color en horizontal 
    {
        int contador = 0; // variable  contador establecida
        for (int i = x-3; i <= x+3; i++)//  parametros de chequeo en eje x del plano
        {
            if (i < 0 || i >= width) // hace un chequeo a lo ancho de todo el plano
                continue;

            GameObject go = grid[i, y];//objeto en posicion i,j

            if (go.GetComponent<Renderer>().material.color == colorVerificar)  // verificador de color  para el objeto
            {
                contador++;  // contador que va en aumento 
                if (contador == 4 && colorVerificar==Color.blue) //comprueba si hay cuatro objetos seguidos de color azul 
                {
                    Debug.Log("felicidades ganaste jugador azul");//  si cantador == 4 y el color es azul lanza un mensaje a la consola
                    ganador=true; // booleano que se ejecuta si contador == 4
                     SceneManager.LoadScene(2); // llamado se escena cuando jugador azul gano
                     
                }

                if(contador == 4 && colorVerificar==Color.red) //comprueba si hay cuatro objetos seguidos de color rojo
                {

                    Debug.Log("felicidades ganaste jugador rojo"); // si cantador == 4 y el color es rojo  lanza un mensaje a la consola
                    ganador=true;  //booleano que se ejecuta si contador == 4
                    SceneManager.LoadScene(3); // llamado se escena cuando jugador rojo gano
                }           
            }
            else
            contador = 0; // si no hay ganador contador queda en 0
            
            
        }
    }

    public void VerificadorY(int x, int y, Color colorVerificar) //clase verificadora de color en vertical
    {
        int contador = 0;  // variable  contador establecida
        for (int j = y - 3; j <= y + 3; j++) //  parametros de chequeo en eje y del plano
        {
            if (j < 0 || j >= height) // hace un chequeo a lo alto de todo el plano
                continue;

            GameObject go = grid[x, j]; //objeto en posicion x,j

            if (go.GetComponent<Renderer>().material.color == colorVerificar)// verificador de color  para el objeto
            {
                contador++; // contador que va en aumento
                if (contador == 4  && colorVerificar==Color.blue) //comprueba si hay cuatro objetos seguidos de color azul 
                {
                    Debug.Log("felicidades ganaste jugador azul"); //  si cantador == 4 y el color es azul lanza un mensaje a la consola
                    ganador=true; // llamado se escena cuando jugador azul gano
                     SceneManager.LoadScene(2); // llamado se escena cuando jugador azul gano
                }
                else if(contador == 4 && colorVerificar==Color.red) //comprueba si hay cuatro objetos seguidos de color rojo
                {
                    Debug.Log("felicidades ganaste jugador rojo"); // si cantador == 4 y el color es rojo  lanza un mensaje a la consola
                    ganador=true; //booleano que se ejecuta si contador == 4
                    SceneManager.LoadScene(3); // llamado se escena cuando jugador rojo gano
                }               
            }
            else 
            contador = 0; // si no hay ganador contador queda en 0
        }
    }

    public void DiagoPositiva(int x, int y, Color colorVerificar) //clase verificadora de color en diagonales positivas
    {
        int contador = 0; // variable  contador establecida
        int j = y - 4;

        for (int i = x - 3; i <= x + 3; i++ ) // parametros de chequeo en diagonal positiva del plano
        {
            j++; // contador de aumento
            if (j < 0 || j >= height || i < 0 || i >= width) // hace un chequeo a lo alto y a lo ancho de todo el plano
                continue;

                GameObject go =grid[i, j]; //objeto en posicion i,j

            if (go.GetComponent<Renderer>().material.color == colorVerificar) // verificador de color  para el objeto
            {
                contador++; // contador que va en aumento
                    
                if (contador == 4  && colorVerificar==Color.blue) //comprueba si hay cuatro objetos seguidos de color azul
                {
                    Debug.Log("felicidades ganaste jugador azul"); // si cantador == 4 y el color es azul  lanza un mensaje a la consola
                    ganador=true;  //booleano que se ejecuta si contador == 4
                    SceneManager.LoadScene(2); // llamado se escena cuando jugador azul gano
                }

                else if(contador == 4 && colorVerificar==Color.red) //comprueba si hay cuatro objetos seguidos de color rojo
                {
                    Debug.Log("felicidades ganaste jugador rojo"); // si cantador == 4 y el color es rojo  lanza un mensaje a la consola
                    ganador=true; //booleano que se ejecuta si contador == 4
                    SceneManager.LoadScene(3); // llamado se escena cuando jugador rojo gano
                }

            }
            else
            contador = 0; // si no hay ganador contador queda en 0        
        }
    }


    public void DiagoNegativa(int x, int y, Color colorVerificar) //clase verificadora de color en diagonales negativas
    {
        int contador = 0; // variable  contador establecida
        int j = y + 4;

        for (int i = x - 3; i <= x + 3; i++) // parametros de chequeo en diagonal negativa del plano
        {
            j--; // contador en decremento

            if (j < 0 || j >= height || i < 0 || i >= width) // hace un chequeo a lo alto y a lo ancho de todo el plano
                continue;

            GameObject go = grid[i, j]; //objeto en posicion i,j

            if (go.GetComponent<Renderer>().material.color == colorVerificar) // verificador de color  para el objeto
            {
                contador++; // contador que va en aumento
                
                if (contador == 4 && colorVerificar==Color.blue) //comprueba si hay cuatro objetos seguidos de color azul
                {
                    Debug.Log("felicidades ganaste jugador azul");
                    ganador=true;  //booleano que se ejecuta si contador == 4
                     SceneManager.LoadScene(2); // llamado se escena cuando jugador azul gano
                }

                else if(contador == 4 && colorVerificar==Color.red) //comprueba si hay cuatro objetos seguidos de color rojo
                {

                    Debug.Log("felicidades ganaste jugador rojo"); // si cantador == 4 y el color es rojo  lanza un mensaje a la consola
                    ganador=true; //booleano que se ejecuta si contador == 4
                    SceneManager.LoadScene(3); // llamado se escena cuando jugador rojo gano
                   
                }
            }
            else
            contador = 0; // si no hay ganador contador queda en 0
        }
    }
}


     
     


    


  