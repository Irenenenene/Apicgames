﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Velocidad de movimiento
    private float speed = 7.0f;
    //Se actualizará la posición a la que debe ir el personaje en esta variable. Dividida en 3 pasos:
    private Vector2 posStairsX; //Posición del ascensor en X
    private Vector2 posFloorY; //Posición de la planta en Y
    private Vector2 posObjectX; //Punto final en X
    private Vector2 posEnd; //Punto que actualiza al personaje

    //Establecemos la posición de las escaleras
    public GameObject gameObjectStairs;

    //GameObject del personaje
    public GameObject character;
    private Vector2 character2D;

    //GameObject del punto de interacción
    public GameObject[] minijuegoPuntoInteract;

    void Start()
    {
        //La pisición inicial a la que se dirige es la propia del elemento (no queremos que se mueva)
        posEnd = character.transform.position;
    }

    void Update()
    {
        //Sacamos los valores del personaje como 2D (solo x e y)
        character2D = new Vector2(character.transform.position.x, character.transform.position.y);

        //Sacamos los valores de la posición de las escaleras y los asignamos a la variable de posición
        posStairsX = new Vector2(gameObjectStairs.transform.position.x, character.transform.position.y);

        //Actualiza la posición del personaje
        //MoveTowards(posición inicial, posición final, velocidad)
        character.transform.position = Vector2.MoveTowards(character.transform.position, posEnd, speed * Time.deltaTime);

        if (character2D == posStairsX)
        {
            posEnd = posFloorY;
        }

        if (character2D == posFloorY)
        {
            posEnd = posObjectX;
        }

        //Mantiene al personaje siempre recto
        character.transform.rotation = Quaternion.identity;

        if (Input.GetMouseButtonDown(0))
        {
            //Pos ratón en 3D
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //La convertimos a 2D
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            //Si el ratón hace click en algo, se moverá a la posición del objeto
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                //1º Derecha > Minijuego señora mayor
                if (hit.collider.gameObject.name == "1D")
                {
                    //Asignamos las variables de movimiento por pasos (ascensor, planta, objeto)
                    posEnd = new Vector2(0.72f, character.transform.position.y);
                    posFloorY = new Vector2(0.72f, minijuegoPuntoInteract[0].transform.position.y);
                    posObjectX = new Vector2(minijuegoPuntoInteract[0].transform.position.x, minijuegoPuntoInteract[0].transform.position.y);
                }
                //3º Izquierda > Minijuego fachafamilia
                else if (hit.collider.gameObject.name == "3I")
                {
                    //Asignamos las variables de movimiento por pasos (ascensor, planta, objeto)
                    posEnd = new Vector2(0.72f, character.transform.position.y);
                    posFloorY = new Vector2(0.72f, minijuegoPuntoInteract[1].transform.position.y);
                    posObjectX = new Vector2(minijuegoPuntoInteract[1].transform.position.x, minijuegoPuntoInteract[1].transform.position.y);
                }
                //4º Derecha > Minijuego ropa tendida
                else if (hit.collider.gameObject.name == "4D")
                {
                    //Asignamos las variables de movimiento por pasos (ascensor, planta, objeto)
                    posEnd = new Vector2(0.72f, character.transform.position.y);
                    posFloorY = new Vector2(0.72f, minijuegoPuntoInteract[2].transform.position.y);
                    posObjectX = new Vector2(minijuegoPuntoInteract[2].transform.position.x, minijuegoPuntoInteract[2].transform.position.y);
                }
            }
        }
    }
}
