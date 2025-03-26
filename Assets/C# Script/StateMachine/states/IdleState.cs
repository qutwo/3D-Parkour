using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {
    }

    public override void EnterState()
    {
        ctx._moveDirectionx = 0f;
        ctx._moveDirectiony = -0f;
        ctx._moveDirectionz = 0f;
        ctx._controller._drag = 15f;
        ctx._animator.SetFloat("velocity", 0f);

     
    }

    public override void UpdateState()
    {
       
    }
    public override void ExitState()
    {

    }
    public override bool SwitchCondintion()
    {
        return ctx._grounded && !ctx._move.IsInProgress() && !ctx._isJumping && !ctx._isCrouching && !ctx._isSliding;
    }
}

