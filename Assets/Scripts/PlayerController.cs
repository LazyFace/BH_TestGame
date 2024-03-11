using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody playerRB;

    //movement variables
    private Vector3 movementInput;

    [SerializeField] private PlayerConfig_SO playerData;
    [SerializeField] private WeaponHolder weaponHolder;

    [SerializeField] private UnityEvent<int> onPlayerDamaged;

    [SerializeField] private float rotationSpeed = 10f;

    private bool isDeath = false;

    private void Awake()
    {
        if (playerRB == null) 
        {
            playerRB = GetComponent<Rigidbody>();
        }

        playerData.currentHealth = playerData.maxHealth;
    }

    private void Update()
    {
        if(!isDeath)
        {
            GetInput();
            PlayerGunActions();
        }
    }

    private void FixedUpdate()
    {
        if (!isDeath) 
        {
            MovePlayer();
            RotatePlayer();
        }
    }

    //shoot gun when pressing the button. 
    private void Shoot()
    {
        if (Input.GetKey(playerData.shootGun))
        {
            weaponHolder.Shoot();
        }
    }

    //Reload gun when pressing the button.
    private void Reload()
    {
        if (Input.GetKey(playerData.reloadGun))
        {
            weaponHolder.Reload();
        }
    }

    //Switch the current weapon for the next one when pressing the button.
    private void ChangeGun()
    {
        if (Input.GetKeyUp(playerData.ChangeGun))
        {
            weaponHolder.ChangeWeapon();
        }
    }

    private void PlayerGunActions()
    {
        Shoot();
        Reload();
        ChangeGun();
    }

    private void GetInput()
    {
        int xInput = 0;
        int zInput = 0;

        if (Input.GetKey(playerData.up)) //Up
        {
            zInput = 1;
        }
        else if (Input.GetKey(playerData.down)) //Down
        {
            zInput = -1;
        }
        else
        {
            zInput = 0;
        }
        
        if (Input.GetKey(playerData.right)) //Right
        {
            xInput = 1;
        }
        else if (Input.GetKey(playerData.left)) //Left
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
        Vector3 moveVector = movementInput * playerData.speed;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
        playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, 10f);
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
            playerRB.rotation = Quaternion.Slerp(playerRB.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void GetDamaged(int damageAmount)
    {
        playerData.currentHealth -= damageAmount;

        onPlayerDamaged?.Invoke(playerData.currentHealth);

        if (playerData.currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDeath = true;
    }
}
