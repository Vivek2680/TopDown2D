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

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carcontroller.SetInputVector(inputVector);
    }
}
