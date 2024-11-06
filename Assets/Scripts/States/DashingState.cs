//using UnityEngine;


//public class DashingState : PlayerState
//{
//    private float dashTimeElapsed;
//    private Vector2 dashDirection;

//    public DashingState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//        dashTimeElapsed = 0f;

//        player.OnDash.Invoke(); // ������ �������� ��� ������� �����

//        player.lockDash = true;
//        player.rb.gravityScale = 0;      // ���������� ����������
//        player.rb.velocity = Vector2.zero; // ����� ��������

//        dashDirection = player.GetDashDirection();  // �������� ����������� �����
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        if (dashTimeElapsed < player.dashDuration)
//        {
//            dashTimeElapsed += Time.fixedDeltaTime;

//            // ����������� ��������� � ����������� �����
//            RaycastHit2D hit = Physics2D.Raycast(player.rb.position, dashDirection, player.dashDistance, player.obstacleLayer);
//            if (hit.collider != null)
//            {
//                player.rb.position = hit.point - dashDirection * 0.1f; // ��������� ����� ������������
//                Exit();
//            }
//            else
//            {
//                player.rb.MovePosition(player.rb.position + dashDirection * player.dashImpulse * Time.fixedDeltaTime);
//            }
//        }
//        else
//        {
//            stateMachine.ChangeState(player.fallingState); // ������� � ��������� ������� ����� �����
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();

//        player.rb.gravityScale = player.gravityDef;  // ��������������� ����������
//        player.rb.velocity = Vector2.zero;           // ������������� ���������
//        player.StartCoroutine(player.DashCooldown()); // ��������� ����������� �����
//        player.lockDash = false;
//    }
//}
