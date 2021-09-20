using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script va en cada portal

public class WinCondition : MonoBehaviour
{
    public SceneChanger changeScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            changeScene.NextScene();
        }
    }
}