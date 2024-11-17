using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabState : State
{
    public WallGrabState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //player.rb.gravityScale = 0;  // Останавливаем падение
        //player.rb.velocity = Vector2.zero;  // Сбрасываем скорость
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
        {
            stateMachine.ChangeState(player.fall);  // Переход в состояние падения, если игрок отпускает кнопку или больше не цепляется
        }
        else if (Input.GetKey(KeyCode.X) && Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.ChangeState(player.climb);  // Переход в состояние лазания по стене
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.anim.SetBool("WallGrab", player.wallGrab);
    }
    public override void Exit()
    {
        base.Exit();
        //player.rb.gravityScale = player.gravityDef;  // Восстанавливаем стандартную гравитацию
    }
}
