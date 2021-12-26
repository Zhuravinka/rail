using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        Death();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void Death()
    {
       
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
