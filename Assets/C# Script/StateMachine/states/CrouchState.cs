
using UnityEngine;
public class CrouchState : BaseState
{
    public float height;
    public Vector3 center;
    public CrouchState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {
    }

    public override void EnterState()
    {
        ctx._isCrouching = true;
   
        height = ctx._controller.ColHeight;
        center = ctx._controller.ColCenter;
        ctx._controller.ColHeight = 0.5f;
        ctx._controller.ColCenter = new Vector3(0f, 0.55f, 0f);
        ctx._controller._TGTvelocityMagnitude = ctx._crouchSpeed;
        ctx._animator.SetBool("isCrouching", true);
    }



    public override void UpdateState()
    {
        ctx._animator.SetFloat("crouchVelocity", 0);
        ctx._controller._drag = 15f;
        if (ctx._move.IsInProgress())
        {
            ctx._controller._drag = 15f;
            ctx._animator.SetFloat("crouchVelocity", 1);
            ctx.look();
        }
        if(ctx._crouch.WasPressedThisFrame())
        {
            ctx._isCrouching = false;
        }
       
       

    }

    public override void FixedUpdate()
    {
        if (ctx._move.IsInProgress())
        {
            ctx._moveDirectionx = ctx.MovementVector().x;
            ctx._moveDirectionz = ctx.MovementVector().z;
            ctx._controller.Move();
        }
        
    }

    public override void ExitState()
    {
        ctx._controller.ColHeight = height;
        ctx._controller.ColCenter = center;
        ctx._isCrouching = false;
        ctx._animator.SetBool("isCrouching", false);
    }

    public override bool SwitchCondintion()
    {
        return ctx._grounded && ctx._crouch.WasPressedThisFrame();
    }
}
