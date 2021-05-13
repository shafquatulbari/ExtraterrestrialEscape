using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainUfoSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessMovement();
    }
    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //(0,1,0) can be used instead of Vector3.up
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainUfoSound);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    void ProcessMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            MoveFront();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
    }
    void RotateLeft() 
    {
        ApplyRotation(-rotationThrust);
    }
    void RotateRight()
    {
        ApplyRotation(rotationThrust);
    }
    void MoveFront() 
    {
        rb.AddRelativeForce(Vector3.left * mainThrust * Time.deltaTime);
    }
    void MoveBack() 
    {
        rb.AddRelativeForce(Vector3.right * mainThrust * Time.deltaTime);
    }
    void ApplyRotation(float rotationThisFrame) 
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        //(0,0,1) can be used instead of Vector3.forward
        transform.Rotate(Vector3.right * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}
