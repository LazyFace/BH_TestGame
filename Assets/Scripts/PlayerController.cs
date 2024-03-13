using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody playerRB;

    //movement variables
    private Vector3 movementInput;
    [SerializeField] private float rotationSpeed = 10f;
    public bool hasBeenDamaged = false;
    private bool isMoving = false;

    [SerializeField] private PlayerConfig_SO playerData;
    [SerializeField] private WeaponHolder weaponHolder;

    [SerializeField] private UnityEvent<int> onPlayerDamaged;
    [SerializeField] private UnityEvent onPlayerShoot;

    [Header("Sound")]
    [SerializeField] private AudioSource pAudioSource;

    private Coroutine hasBeenDamagedCoroutine;
    private Coroutine healingCoroutine;

    private bool isDeath = false;
    private bool justOnceDeath = false;

    private void Awake()
    {
        if (playerRB == null) 
        {
            playerRB = GetComponent<Rigidbody>();
        }

        justOnceDeath = false;

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
            ChangeSoundState();
        }
    }

    //shoot gun when pressing the button. 
    private void Shoot()
    {
        if (Input.GetKey(playerData.shootGun))
        {
            weaponHolder.Shoot();
            onPlayerShoot?.Invoke();
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

        if (movementInput.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
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
        if(hasBeenDamagedCoroutine != null) 
        {
            StopCoroutine(hasBeenDamagedCoroutine);
            hasBeenDamagedCoroutine = null;
        }
        if(healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }

        playerData.currentHealth -= damageAmount;
        onPlayerDamaged?.Invoke(playerData.currentHealth);
        hasBeenDamaged = true;
        hasBeenDamagedCoroutine = StartCoroutine(WaitToHeal());

        if (playerData.currentHealth < 0 && !justOnceDeath)
        {
            justOnceDeath = true;
            Die();
        }
    }

    private IEnumerator WaitToHeal()
    {
        yield return new WaitForSeconds(3);
        healingCoroutine = StartCoroutine(Healing());
        hasBeenDamaged = false;
    }

    private IEnumerator Healing()
    {
        if(playerData.currentHealth < 100) 
        {
            playerData.currentHealth += 5;
            onPlayerDamaged?.Invoke(playerData.currentHealth);
        }
        yield return new WaitForSeconds(1);
        healingCoroutine = StartCoroutine(Healing());
    }

    private void Die()
    {
        isDeath = true;
        pAudioSource.enabled = false;
        GameManager.Instance.GameLost();
    }

    private void ChangeSoundState() {
        if (isMoving)
        {
            pAudioSource.enabled = true;
        }else
        {
            pAudioSource.enabled = false;
        }
        
    }
}
