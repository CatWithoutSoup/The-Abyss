using Unity.VisualScripting;
using UnityEngine;


public class Grounded : PlayerState
{
    protected PlayerMovement _pm;
    public Grounded(string name, PlayerMovement stateMachine) : base(name, stateMachine) 
    {
        _pm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //player.OnRun.Invoke(); // ����� �������� ��� ������ ��� ����
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetKeyUp(KeyCode.Space))
            stateMachine.ChangeState(_pm.jumpingState);

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
}
