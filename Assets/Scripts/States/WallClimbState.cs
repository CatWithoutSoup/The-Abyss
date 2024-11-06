//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallClimbState : PlayerState
//{
//    public WallClimbState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//        player.rb.gravityScale = 0;  // Отключаем гравитацию
//    }

//    public override void HandleInput()
//    {
//        base.HandleInput();

//        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
//        {
//            stateMachine.ChangeState(player.fallingState);  // Если игрок отпускает кнопку, то переключаемся на падение
//        }
//        else if (Input.GetAxisRaw("Vertical") == 0)
//        {
//            stateMachine.ChangeState(player.wallGrabState);  // Если игрок перестает двигаться по стене, переключаемся на захват
//        }
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        float verticalMove = Input.GetAxisRaw("Vertical");
//        player.rb.velocity = new Vector2(player.rb.velocity.x, verticalMove * player.climbSpeed);
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        player.rb.gravityScale = player.gravityDef;  // Возвращаем гравитацию при выходе
//        player.rb.velocity = Vector2.zero;
//    }
//}
