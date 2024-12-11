using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabState : State
{
    public WallGrabState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //player.rb.gravityScale = 0;  // ������������� �������
        //player.rb.velocity = Vector2.zero;  // ���������� ��������
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyUp(KeyCode.Z))
        {
            player.jumpHoldTime = 0;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isWalled && Input.GetAxisRaw("Vertical") > 0)
        {
            stateMachine.ChangeState(player.slide);
        }
        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
        {
            stateMachine.ChangeState(player.fall);  // ������� � ��������� �������, ���� ����� ��������� ������ ��� ������ �� ���������
        }
        else if (Input.GetKey(KeyCode.X) && Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.ChangeState(player.climb);  // ������� � ��������� ������� �� �����
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            stateMachine.ChangeState(player.fall);
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

        //player.rb.gravityScale = player.gravityDef;  // ��������������� ����������� ����������
    }
}
