using System.Collections;
using UnityEngine;

public class SlideState : BaseState
{
    bool slideCompleted;
    public float height;
    public Vector3 center;
    public SlideState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {

    }

    public override void EnterState()
    {

        ctx._controller._drag = 0f;
        slideCompleted = false;
        ctx._isSliding = true;
        height = ctx._controller.ColHeight;
        center = ctx._controller.ColCenter;
        ctx._controller.ColHeight = 0.5f;
        ctx._controller.ColCenter = new Vector3(0f, 0.55f, 0f);
        ctx._controller._TGTvelocityMagnitude = ctx._slideSpeed;
        ctx._animator.SetBool("sliding",true);
        ctx.StartCoroutine(Sliding());
    }
    public override void FixedUpdate()
    {
        ctx._moveDirectionx = ctx.MovementVector().x;
        ctx._moveDirectionz = ctx.MovementVector().z;
        ctx._controller.Move();
    }
    public override void UpdateState()
    {
        if (slideCompleted)
        {
            ctx._isSliding = false;

        }
        ctx.look();

    }
    public override void ExitState()
    {
        ctx._controller.ColHeight = height;
        ctx._controller.ColCenter = center;

        ctx._animator.SetBool("sliding", false);

    }

    IEnumerator Sliding()
    {
        
        yield return new WaitForSecondsRealtime(ctx._slideTime);
        slideCompleted = true;

    }

    public override bool SwitchCondintion()
    {
        return ctx._grounded && ctx._crouch.WasPressedThisFrame();
    }















}
