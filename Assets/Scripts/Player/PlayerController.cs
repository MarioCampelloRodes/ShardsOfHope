using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool canMove = true;

    [SerializeField] float dashForce = 20f;
    [SerializeField] float dashDuration = 0.3f;
    [SerializeField] float dashCooldown = 1.2f;
    float dashCounter;

    [SerializeField] bool isDashing = false;
    [SerializeField] bool canDash = true;

    [SerializeField] float jumpForce = 10f;

    [SerializeField] float invincibleDuration = 0.8f;
    public float invincibleCounter;

    Rigidbody rb;

    [Header("GROUND CHECKER")]
    [SerializeField] bool isGrounded;
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    private Collider[] detectedColliders;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("Salto");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

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
