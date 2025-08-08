using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
        
    
    //Components
    Carcontroller carcontroller;
    void Awake()
    {
        carcontroller = GetComponent<Carcontroller>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        // Get raw input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        
            
        // Create input vector
        inputVector = new Vector2(horizontalInput, verticalInput);
        
        // Normalize diagonal movement to prevent faster diagonal speed
        // But preserve the original magnitude for partial inputs
        if (inputVector.magnitude > 1f)
        {
            inputVector = inputVector.normalized;
        }

        carcontroller.SetInputVector(inputVector);
    }
}
