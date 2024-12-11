using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : State
{
    public WallJumpState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
{
    base.Enter();

    // ��������� ������
    player.rb.velocity = Vector2.zero; // ���������� ��������
    player.rb.AddForce(Vector2.up * player.wallJumpForce +
                       Vector2.right * player.wallJumpHorizontalForce * (player.faceRight ? -1 : 1), ForceMode2D.Impulse);

    // ��������� �������� ����� �� �������� �����
    player.isWalled = false;
    player.StartCoroutine(EnableWallCheck());
}

public override void LogicUpdate()
{
    base.LogicUpdate();

    // ���� ����� ����� �������� ����� � ���������� ������, �� ������������ � WallSlideState
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
    yield return new WaitForSeconds(0.2f); // ����� ���������� ��������
    player.isWalled = true;
}
}