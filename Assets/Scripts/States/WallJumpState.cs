using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : State
{
    public WallJumpState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
{
    base.Enter();

    // Выполняем прыжок
    player.rb.velocity = Vector2.zero; // Сбрасываем скорость
    player.rb.AddForce(Vector2.up * player.wallJumpForce +
                       Vector2.right * player.wallJumpHorizontalForce * (player.faceRight ? -1 : 1), ForceMode2D.Impulse);

    // Отключаем проверку стены на короткое время
    player.isWalled = false;
    player.StartCoroutine(EnableWallCheck());
}

public override void LogicUpdate()
{
    base.LogicUpdate();

    // Если игрок снова коснулся стены и удерживает кнопку, он возвращается в WallSlideState
    if (player.rb.velocity.y <= 0)
    {
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.slide);
        }
        else
        {
            stateMachine.ChangeState(player.fall);
        }
    }
}

private IEnumerator EnableWallCheck()
{
    yield return new WaitForSeconds(0.2f); // Время отключения проверки
    player.isWalled = true;
}
}