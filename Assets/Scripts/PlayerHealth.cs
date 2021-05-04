using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int hearts = 3;
    public Image[] heartsImages;
    bool hasCooldown = false;
    public SceneChanger changeScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && GetComponent<PlayerMovement>().isGrounded)
        {
            SubstractHealth();            
        }
    }

    void SubstractHealth()
    {
        if (hasCooldown == false && hearts > 0)
        {
            hearts--; //Con el primer choque se vuelve 2
            hasCooldown = true;
            StartCoroutine(Cooldown());            

            EmptyHearts();

            if(hearts <= 0)
            {
                changeScene.ChangeSceneTo("LoseScene");
            }
        }
    }

    void EmptyHearts()
    {
        if (hearts < heartsImages.Length)
        {
            heartsImages[hearts].gameObject.SetActive(false);
        }        
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(.5f);
        hasCooldown = false;
        StopCoroutine(Cooldown());
    }
}