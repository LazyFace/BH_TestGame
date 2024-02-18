using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;

    //movement variables
    private Vector3 movementInput;
    [SerializeField] private float speed = 0;

    private void Awake()
    {
        if (playerRB == null) 
        {
            playerRB = GetComponent<Rigidbody>();
        }
        
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void GetInput()
    {
        int xInput = 0;
        int zInput = 0;

        if (Input.GetKey(KeyCode.W))
        {
            zInput = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zInput = -1;
        }
        else
        {
            zInput = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xInput = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xInput = -1;
        }
        else
        {
            xInput = 0;
        }

        // Tomar el input del jugador(No usamos los Axis ya que cada jugador va usar WASD o las flechas
        movementInput = new Vector3(xInput, 0f, zInput);
        movementInput.Normalize();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = movementInput * speed;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
        Mathf.Clamp(playerRB.velocity.magnitude, 0f, 10f);
    }

    private void RotatePlayer()
    {
        // Determina la dirección del movimiento basada en la velocidad del Rigidbody.
        Vector3 movementDirection = playerRB.velocity;
        movementDirection.y = 0; // Ignora la componente Y para la rotación.

        // Verifica si el jugador se está moviendo.
        if (movementDirection != Vector3.zero)
        {
            // Crea una rotación que mira en la dirección del movimiento.
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            // Suaviza la transición hacia la nueva rotación.
            playerRB.rotation = Quaternion.Slerp(playerRB.rotation, toRotation, Time.deltaTime * 15f);
        }
    }
}
