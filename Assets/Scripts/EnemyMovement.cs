using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script va en cada enemigo

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRb; //Variable para obtener el rigid body del enemigo, no importa si no lleva public o private porque el script que guardara esta en el mismo enemigo
    ParticleSystem enemyParticle;
    AudioSource enemyDeadAudio;
    float timeBeforeChange = 0; //Tiempo antes de cambiar, esta variable inicia en 0 para que el movimiento del enemigo empiece desde que inica el juego
    public SpriteRenderer enemySprite;
    public Animator enemyAnimator;
    public float enemySpeed = .5f; //Velocidad del enemigo
    public float delay = 2f; //Tiempo que tardara el personaje en dar vuelta

    void Start() //Todo lo que aqui se carga antes del primer frame
    {
        //NOTA: La obtencion de componentes de las variables va aqui para que permanecezcan en memoria por su uso constante
        enemyRb = GetComponent<Rigidbody2D>(); //1er forma de obtener un componente; porque esta junto con este script en el mismo game object
        enemyParticle = GameObject.Find("EnemyParticle").GetComponent<ParticleSystem>(); //2da forma de obtener un componente; lo busca en el por medio de su nombre y luego lo obtiene
        enemyDeadAudio = GetComponentInParent<AudioSource>(); //3ra forma de obtener un componente; lo obtiene del contenedor padre del game object en donde esta este script
        enemySprite = GetComponent<SpriteRenderer>(); //4ta forma de obtener un componente; es parecido a la 1ra pero en este caso el componente se paso como parametro a la variable desde el editor por que es publica
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //TIMER DEL MOVIMIENTO DEL ENEMIGO DE IZQUIERDA A DERECHA
        enemyRb.velocity = Vector2.right * enemySpeed; //Indica que el enemigo vaya a la derecha a determinada velocidad, se ejecuta cada frame, o sea, todo el tiempo se esta actualizando
        if (enemySpeed > 0)
        {
            enemySprite.flipX = false;
        }
        else if (enemySpeed < 0)
        {
            enemySprite.flipX = true;
        }

        if (timeBeforeChange < Time.time) //Time.time es el tiempo que ha pasado desde que el juego empezo, como timeBeforeChange empieza en 0 la condicion es correcta
        {
            enemySpeed *= -1; //Esto hace que la velocidad del enemigo sea negativa, lo que hara que vaya a la izquierda porque enemyRb.velocity se actualiza cada frame y en la proxima vuelta sera positiva, haciendo que cambie de direccion a la derecha y asi sucesivamente
            timeBeforeChange = Time.time + delay; //Suma el delay al tiempo que lleva ejecutandose el juego y eso es lo que tardara el enemigo en dar vuelta, se actualiza cada frame
        }
        //FIN DEL CODIGO DEL TIMER
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y + .03f < collision.transform.position.y)
            {
                enemyAnimator.SetBool("isDead", true); //Activa la animacion de muerte del enemigo
            }
        }
    }

    public void EnemyDestroy() //Para activar esta funcion, se debe agregar arriba del ultimo frame de la animacion de muerte en el Animation desde el editor, por eso la funcion es publica
    {
        enemyDeadAudio.Play();
        enemyParticle.transform.position = transform.position;
        enemyParticle.Play();
        gameObject.SetActive(false); //Desactiva el game object al que esta adjunto este script y se puede volver a utilizar se se activa otra vez
        //Destroy(gameObject); //Borra al enemigo por completo de la jerarquia
    }
}