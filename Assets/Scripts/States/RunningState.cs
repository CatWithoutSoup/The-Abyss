using Unity.VisualScripting;
using UnityEngine;


public class RunningState : Grounded
{
    private float _horizontalInput;
    public RunningState(PlayerMovement stateMachine) : base("RunningState", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        //player.OnRun.Invoke(); // ����� �������� ��� ������ ��� ����
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
            stateMachine.ChangeState(_pm.idleState);

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
        Vector2 vel = _pm.rb.velocity;
        vel.x = _horizontalInput * _pm.speed;
        _pm.rb.velocity = vel;
        //player.rb.velocity = new Vector2(player.moveVector.x, player.rb.velocity.y); // ���������� �������� �� �����������
    }

    //public override void Exit()
    //{
    //    base.Exit();
    //    //player.OnRunEnd.Invoke(); // ��������� �������� ����, ���� ����������
    //}
}
