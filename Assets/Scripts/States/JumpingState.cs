//using UnityEngine;


//public class JumpingState : PlayerState
//{
//    private float jumpForce = 100f; // ��������� ���� ������
//    private int jumpValueIter = 60; // ����� ����� �������� ������
//    private int jumpIter = 0; // ������� ������� ��������

//    public JumpingState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//        player.rb.velocity = new Vector2(player.rb.velocity.x, 0); // ��������� ������������ �������� ����� �������
//        jumpIter = 0; // ����� ��������� ������
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();

//        // �������� �� NaN � Infinity ����� ����������� ����
//        if (jumpIter < jumpValueIter)
//        {
//            float force = jumpForce / Mathf.Max(1f, jumpIter / 3f); // �������������� ������� �� ���� ��� ������� ����� ��������
//            if (!float.IsNaN(force) && !float.IsInfinity(force))
//            {
//                player.rb.AddForce(Vector2.up * force);
//                jumpIter++;
//            }
//            else
//            {
//                Debug.LogWarning("���� ������ ����� ������������: " + force);
//            }
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        jumpIter = 0; // ����� ��������� ������ ��� ������ �� ���������
//    }
//}
