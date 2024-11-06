//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallGrabState : PlayerState
//{
//    public WallGrabState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//        player.rb.gravityScale = 0;  // Останавливаем падение
//        player.rb.velocity = Vector2.zero;  // Сбрасываем скорость
//    }

//    public override void HandleInput()
//    {
//        base.HandleInput();

//        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
//        {
//            stateMachine.ChangeState(player.fallingState);  // Переход в состояние падения, если игрок отпускает кнопку или больше не цепляется
//        }
//        else if (Input.GetAxisRaw("Vertical") != 0)
//        {
//            stateMachine.ChangeState(player.wallClimbState);  // Переход в состояние лазания по стене
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        player.rb.gravityScale = player.gravityDef;  // Восстанавливаем стандартную гравитацию
//    }
//}
