using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Grounded : State
{
    protected float speed;
    public Grounded(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) 
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.OnRun.Invoke(); // ����� �������� ��� ������ ��� ����
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //// ��������� ��������������� ����� ��� ���������� ��������� ���������
        //float horizontalInput = Input.GetAxis("Horizontal");
        //player.moveVector.x = horizontalInput * player.speed;

        //// ������� ��� ����� ���������
        //if (horizontalInput == 0)
        //{
        //    stateMachine.ChangeState(player.idleState); // ������� � IdleState ��� ���������
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        //{
        //    stateMachine.ChangeState(player.jumpingState); // ������� � JumpingState ��� ������
        //}
        //else if (Input.GetKeyDown(KeyCode.C) && player.airDashesRemaining > 0 && !player.isDashCooldown)
        //{
        //    stateMachine.ChangeState(player.dashingState); // ������� � DashingState ��� ������� �� �����
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
