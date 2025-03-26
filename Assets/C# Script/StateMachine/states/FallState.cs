using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    public FallState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {
        
    }



    public override void EnterState()
    {


        ctx._controller._drag = 0f;
        ctx._animator.SetBool("falling",true);
     

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
        if (ctx._move.IsInProgress())
        {
            ctx.look();
        }
    }   


    public override void ExitState()
    {
        ctx._animator.SetBool("falling", false);


    }
    public override bool SwitchCondintion()
    {
        return !ctx._grounded && !ctx._isJumping;
    }
}
