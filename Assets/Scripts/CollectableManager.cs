using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Libreria para poder usar componentes de la UI

//Este script va en cada collectable

public class CollectableManager : MonoBehaviour
{
    public static int collectableQuantity = 0; //Es static para que el contador sea SOLO UNO para todas las instancias y que al agarrarlos se contabilicen todos los collectables en un solo contador
    ParticleSystem collectableParticle; //Variable para usar el Particle System del collectableParticle. NO SE PASA COMO REFERENCIA EN EL EDITOR, POR ESO NO ES PUBLIC
    Text collectableQuantityText;
    AudioSource collectableAudio;
    void Start()
    {
        collectableQuantity = 0; //Reinicia el contador a 0 al cambiar de escena, pero en realidad deberia conservarse al cambiar de nivel y solo reiniciarse al perder

        //NOTA: Esta obtencion de componentes va aqui en Start() porque son cosas que se llamaran seguido, esto para no realentizar el juego
        collectableParticle = GameObject.Find("CollectableParticle").GetComponent<ParticleSystem>(); //Debido a que el Particle System no es un componente del collectable, o sea el game object en el que esta este script porque ES PARTE DE OTRO GAME OBJECT, este se busca aqui en el start por medio de su nombre y se obtiene su componente Particle System y se asigna a esta variable. NO ESTA EN EL GAME OBJECT DE ESTE SCRIPT POR QUE TODOS LOS PARTICLE SYSTEMS ESTAN EN UN SOLO GRUPO EN LA JERARQUIA
        collectableQuantityText = GameObject.Find("CollectableQuantityText").GetComponent<Text>();
        collectableAudio = GetComponentInParent<AudioSource>(); //Esta es otra manera de obtener un componente de un game object, obteniendo el componente directo del contenedor padre del game object en el que esta este script, o sea, el audio source esta en el contenedor donde estan agrupados los collectables en la jerarquia
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //En los OnTriggerEnter2D se ouede omitir usar .gameObject antes del CompareTag()
        {
            collectableParticle.transform.position = transform.position; //Indica que la posicion del Collectable Particle sera la de cada instancia de este collectable que tiene este script
            collectableParticle.Play(); //Reproduce el Collectable Particle
            collectableAudio.Play();
            gameObject.SetActive(false); //Desactiva el  game Object del collectable
            collectableQuantity++; //Aumenta el contador en 1
            collectableQuantityText.text = collectableQuantity.ToString().PadLeft(2, '0'); //Y refleja el contador en el texto de la UI pero antes lo convierte a string y para que el texto siempre tenga dos digitos se usa PadLeft() donde el primer parámetro es el numero de caracteres, y el segundo parámetro el caracter que se colocara a menos que incremente el contador, o sea, si no se obtiene ningun collectable, permanecera en 00
        }
    }
}