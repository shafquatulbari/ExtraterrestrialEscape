using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) //or use period == 0f instead 
        {
            return;  // to avoid NaN error
        }
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // const value of 6.283
        float rawSignWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSignWave + 1f)/2f; // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
