using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : AbstractCharacterController
{


    Rigidbody rb;
    CapsuleCollider col;
    float TGTvelocityMagnitude;
    //float wait = 0.5f;
    RaycastHit hit;
    LayerMask Ground;
    Vector3 TGTvelocityDirection;
    Vector3 defaultD = Vector3.up;


    public PlayerCharacterController(Rigidbody rb,CapsuleCollider col)
    {
        this.rb = rb;
        this.col = col;
    }
    public override void Move(float OverrideMagnitude =1f)
    {
         rb.velocity = new Vector3(TGTvelocityDirection.normalized.x, 0, TGTvelocityDirection.normalized.z) * TGTvelocityMagnitude * OverrideMagnitude;  
        
    }

    public void AirMove(float airMoveForce)
    {
        rb.AddForce(TGTvelocityDirection.normalized * airMoveForce, ForceMode.Acceleration);
    }

    public void JumpForce(float jumpForce)
    {
       
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        
    }
    public override void ApplyGravity(float gravity, Vector3 direction)
    {
        rb.AddForce(direction * gravity, ForceMode.Acceleration);
    }

    public override bool isGrounded()
    {
        
       
        bool isgrounded = Physics.Raycast(rb.transform.position+Vector3.up*0.2f, Vector3.down, out hit,  0.22f, Ground);
   
        
       
        return isgrounded;
    }
   

    public void _setGroundLayer(LayerMask lm) { Ground = lm; }
    public float _TGTvelocityMagnitude {set { TGTvelocityMagnitude = value; } }
    public Vector3 _TGTvelocityDirection {set { TGTvelocityDirection = value; } }
    public float _drag { set { rb.drag = value; } }
    public RaycastHit _hit { get { return hit; } }
    public float ColHeight { get { return col.height; }  set { col.height = value; } }
    public Vector3 ColCenter { get { return col.center; } set { col.center = value; } }

}
