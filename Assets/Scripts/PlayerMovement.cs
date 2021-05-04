using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb; //Variable para referenciar al player desde el editor para usar su rigid body
    public float speed = 1f;
    public float jumpForce = 200;
    public bool isGrounded = true;
    public Animator playerAnimator;
    public SpriteRenderer playerSprite; //Siempre es mejor pasar los componentes por referencia con una variable desde el editor, asi como aqui para liego desde ella usar sus funciones, pero si es algo sencillo no es necesario crear la variable y basta con empezar el codigo desde GetComponent<>

    void Update()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y); //La velocidad del player sera el movimiento que detecte en horizontal(eje x) multiplicado por speed, y conservara su posicion en y

        if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnimator.SetBool("isWalking", true);
            playerSprite.GetComponent<SpriteRenderer>().flipX = true;
            Jump();
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimator.SetBool("isWalking", false);
            Jump();
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnimator.SetBool("isWalking", true);
            playerSprite.GetComponent<SpriteRenderer>().flipX = false;
            Jump();
        }
    }
        
    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play(); //Cuando un elemento no se llama muy seguido, se pone el GetComponent<> directamente donde se va a usar, aun asi la obtencion del componente de audio del player debe ir en el Start()
            playerRb.AddForce(Vector2.up * jumpForce);
            //playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("isJumping", true);

            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnimator.SetBool("isJumping", false);
            isGrounded = true;
        }
    }
}