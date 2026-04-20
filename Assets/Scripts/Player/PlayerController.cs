using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool canMove = true;

    [Header("Z POSITION")]
    [SerializeField] float zSpeed = 100f;
    [SerializeField] Vector2 zRange = new Vector2(-3.5f, 0f);
    [SerializeField] float zSmoothSpeed = 3f;
    [SerializeField] float passiveZRecoveryAmount = 15f;
    [SerializeField] float passiveZRecoveryTime = 5f;

    [Header("DASH")]
    [SerializeField] float dashForce = 20f;
    [SerializeField] float dashDuration = 0.3f;
    [SerializeField] float dashCooldown = 1.2f;
    float dashCounter;
    [SerializeField] bool isDashing = false;
    [SerializeField] bool canDash = true;

    [Header("JUMP")]
    [SerializeField] float jumpForce = 10f;

    [Header("GROUND CHECKER")]
    [SerializeField] bool isGrounded;
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    private Collider[] detectedColliders;
    public LayerMask groundLayer;

    [SerializeField] float invincibleDuration = 0.8f;
     float invincibleCounter;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerHurtbox.Instance.onHurt += ZPositionChange;

        StartCoroutine(PassiveZRecovery());
    }

    private void Update()
    {
        //DASH
        if (Input.GetButtonDown("Dash") && !isDashing && canDash)
        {
            Debug.Log("Dash");
            Dash();
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            canDash = false;
        }
        else
        {
            canDash = true;
        }


        //INVINCIBILIDAD
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }

        //SALTO
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        CalculateZPosition();
        
        GroundCheck();
    }

    private void FixedUpdate()
    {
        //Movimiento

        if (canMove)
        {
            rb.velocity = new Vector3 (Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
    }

    public void Dash()
    {
        StartCoroutine(DashCO());
    }

    IEnumerator DashCO()
    {
        canMove = false;

        isDashing = true;

        rb.useGravity = false;

        invincibleCounter = invincibleDuration;

        rb.velocity = Vector3.zero;

        if (Input.GetAxis("Horizontal") > 0.1)
        {
            rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
        }
        else if(Input.GetAxis("Horizontal") < 0.1)
        {
            rb.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
        }
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;

        rb.useGravity = true;

        canMove = true;

        isDashing = false;

        dashCounter = dashCooldown;
    }
    
    void CalculateZPosition()
    {
        //Calcula el porcentaje de la velocidad en Z
        float zSpeedNormalized = zSpeed / 100f;

        //Obtiene el punto en el que tiene que posicionarse el player entre las posiciones Z mínimas y máximas
        float targetZ = Mathf.Lerp(zRange.x, zRange.y, zSpeedNormalized);

        //Se mueve hacia la posición Z target de forma suave
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, targetZ), Time.deltaTime * zSmoothSpeed);
    }

    public void ZPositionChange(float speedChange)
    {
        zSpeed += speedChange;

        if(zSpeed > 100)
        {
            zSpeed = 100;
        }
        else if(zSpeed <= 0)
        {
            //Muerte
        }
    }

    IEnumerator PassiveZRecovery()
    {
        while (true)
        {
            ZPositionChange(passiveZRecoveryAmount);
            yield return new WaitForSeconds(passiveZRecoveryTime);
        }
    }

    void GroundCheck()
    {
        detectedColliders = Physics.OverlapBox(groundCheckCenter.position, groundCheckSize * 0.5f, Quaternion.Euler(0, 0, 0), groundLayer);

        //Cuando el checker detecte al menos un objeto suelo, podemos saltar
        if (detectedColliders.Length > 0)
        {
            isGrounded = true;
        }
        else //Cuando no haya ningun objeto detectado, ya estaremos en el aire
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheckCenter.position, groundCheckSize);
    }
}
