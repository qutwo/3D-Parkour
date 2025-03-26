using System.Collections;
using UnityEngine;

public class JumpState : BaseState
{
    bool jumpCompleted;
    public JumpState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {
        
    }

    public override void EnterState()
    {
        
        ctx._controller._drag = 0f;
        jumpCompleted = false;
        ctx._isJumping = true;
        ctx._animator.SetTrigger("has jumped");
        ctx.StartCoroutine(Jumping());

    }
  



       

       


   
    public override void FixedUpdate()
    {
        if (ctx._move.IsInProgress())
        {
            ctx._moveDirectionx = ctx.MovementVector().x;
            ctx._moveDirectionz = ctx.MovementVector().z;
            ctx._controller.AirMove(ctx._forceAppliedInAir);
        }
       
     

    }

    public override void UpdateState()
    {
        if(jumpCompleted)
        {
            ctx._isJumping = false;

        }
        if (ctx._move.IsInProgress())
        {
            ctx.look();
        }    
        

    }
    public override void ExitState()
    {
        


    }

    IEnumerator Jumping()
    {
        ctx._controller.JumpForce(ctx._jumpSpeed);
        yield return new WaitForSecondsRealtime(ctx._jumpTime);
     
        jumpCompleted = true;
    }

    public override bool SwitchCondintion()
    {
        return ctx._grounded && ctx._jump.WasPressedThisFrame();
    }

}
