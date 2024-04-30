using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Este script es meramente de prueba para experimentar como seria el movimiento del personaje
//a travez de un character controller
// 
//Ctrl K + C para comentar texto seleccionado

public class PlayerCC : MonoBehaviour
{
    public float velX;
    private CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        Mover();
    }

    public void Mover()
    {
        float h = Input.GetAxisRaw("Horizontal");        

        Vector3 mover = velX * Time.deltaTime * h * (transform.right);
        CC.Move(mover);
    }
}