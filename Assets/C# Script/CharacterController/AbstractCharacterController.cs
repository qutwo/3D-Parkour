
using UnityEngine;

public abstract class AbstractCharacterController
{
    Rigidbody rigidbody;
    Collider collider;

    public abstract void Move(float OverrideMagnitude);
    public abstract bool isGrounded();

    public abstract void ApplyGravity(float gravity,Vector3 dir);
}
