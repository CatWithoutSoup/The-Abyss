using Unity.VisualScripting;
using UnityEngine;


public class RunningState : Grounded
{
    private float _horizontalInput;
    public RunningState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        speed = player.speed;
        //_horizontalInput = 0f;
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
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Move(speed);
        //player.rb.velocity = new Vector2(player.moveVector.x, player.rb.velocity.y); // ���������� �������� �� �����������
    }

    //public override void Exit()
    //{
    //    base.Exit();
    //    //player.OnRunEnd.Invoke(); // ��������� �������� ����, ���� ����������
    //}
}
