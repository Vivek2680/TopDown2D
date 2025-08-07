using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    [Header("Car Setting")]
    public float AccelerationFactor = 30.0f;
    public float TurnFactor = 3.5f;
    public float DriftFactor = 0.95f;
    public float maxSpeed = 20f;

    //Local Variable
    float AccelerasionInput = 0f;
    float SteeringInput = 0f;
    float RotationAngle = 0f;
    float velocityVsUp = 0f;

    //Components
    Rigidbody2D carRigidbody2D;
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
    }
    void ApplyEngineForce()
    {
        //Apply drag if there is no accelerationinput so the car stop
        if (AccelerasionInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0;
        }
        //limit so we cannot go faster than max speed in forward direction
        if (velocityVsUp > maxSpeed && AccelerasionInput > 0)
            return;
        //Limit 50% for backward
        if (velocityVsUp < -maxSpeed*0.5f && AccelerasionInput < 0)
            return;
        //Limit so we can go faster in any diration while acceleration
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && AccelerasionInput < 0)
            return;
        //Create an force for engine
        Vector2 EngineForceVector = transform.up * AccelerasionInput * AccelerationFactor;

        //Apply force and push to the car
        carRigidbody2D.AddForce(EngineForceVector, ForceMode2D.Force);
    }
    void ApplySteering()
    {
        //Limit the car ability to turn on input
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Udate the Rotation angle based on input
        RotationAngle -= SteeringInput * TurnFactor*minSpeedBeforeAllowTurningFactor;
        //Apply steering by rotation the car object
        carRigidbody2D.MoveRotation(RotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * DriftFactor;
    }
    public void SetInputVector(Vector2 inputVector)
    {
        SteeringInput = inputVector.x;
        AccelerasionInput = inputVector.y;
    }
}
