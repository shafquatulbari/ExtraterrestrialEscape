using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clash : MonoBehaviour
{
    [SerializeField] AudioClip clashSound;
    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ParticleSystem clashParticle;
    [SerializeField] float levelLoadDelay = 2f;
    AudioSource audioSource;
    bool isTransitioning = false;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning)
        {
            //returns nothing
            return;
        }
        switch(other.gameObject.tag)
        {
        case "Friendly":
            break;
        case "Finish": 
            FinishSequence();
            break;
        default:
            ClashSequence();
            break;
        }
        
    }

    void FinishSequence() 
    {
        isTransitioning = true;
        audioSource.Stop(); // stops sound effect
        finishParticle.Play();
        //todo add sound and particle effect after success
        GetComponent<Movement>().enabled = false;
        LoadNextLevel();
    }

    void ClashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stops sound effect
        audioSource.PlayOneShot(clashSound);
        clashParticle.Play();
        //todo add sound and particle effect after crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay); 
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel() 
    {
        //0 is the screen build index or use "SandBox" 
        //SceneManager.GetActiveScene().buildIndex return current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
