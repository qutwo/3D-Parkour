

public class MoveState : BaseState
{
   
    public MoveState(PlayerStateMachine ctx, StateFactory factory) : base(ctx, factory)
    {
    }



    public override void EnterState()
    {

        
       
        ctx._controller._drag = 0f;
        
        
    }

    public override void UpdateState()
    {
        ctx.look();
        if (ctx._sprint.IsInProgress())
        {
            ctx._controller._TGTvelocityMagnitude = ctx._sprintSpeed;
            ctx._animator.SetFloat("velocity", 0.5f);
        }
        else 
        {
            ctx._controller._TGTvelocityMagnitude = ctx._walkSpeed;
            ctx._animator.SetFloat("velocity", 0.25f);

        }
        
    }

    public override void FixedUpdate()
    {
        ctx._moveDirectionx = ctx.MovementVector().x;
        ctx._moveDirectionz = ctx.MovementVector().z;
        ctx._controller.Move();



    }
    public override void ExitState()
    {
      
    }


    public override bool SwitchCondintion()
    {
        return ctx._grounded && ctx._move.IsInProgress()  && !ctx._isJumping && !ctx._isCrouching && !ctx._isSliding;
    }
}