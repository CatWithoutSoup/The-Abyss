//using UnityEngine;

//public class FallingState : PlayerState
//{
//    public FallingState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//    }

//    public override void HandleInput()
//    {
//        base.HandleInput();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        // ������� �� �����, ���� �������� �������� �����
//        if (player.isGrounded)
//        {
//            stateMachine.ChangeState(player.idleState);
//        }
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();

//        // ���������� �������������� ��������� � �������
//        float horizontalMove = Input.GetAxis("Horizontal") * player.speed;
//        player.rb.velocity = new Vector2(horizontalMove, player.rb.velocity.y);
//    }
//}
