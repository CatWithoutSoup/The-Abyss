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

//        player.OnDash.Invoke(); // Запуск анимации или эффекта рывка

//        player.lockDash = true;
//        player.rb.gravityScale = 0;      // Отключение гравитации
//        player.rb.velocity = Vector2.zero; // Сброс скорости

//        dashDirection = player.GetDashDirection();  // Получаем направление рывка
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        if (dashTimeElapsed < player.dashDuration)
//        {
//            dashTimeElapsed += Time.fixedDeltaTime;

//            // Перемещение персонажа в направлении рывка
//            RaycastHit2D hit = Physics2D.Raycast(player.rb.position, dashDirection, player.dashDistance, player.obstacleLayer);
//            if (hit.collider != null)
//            {
//                player.rb.position = hit.point - dashDirection * 0.1f; // Остановка перед препятствием
//                Exit();
//            }
//            else
//            {
//                player.rb.MovePosition(player.rb.position + dashDirection * player.dashImpulse * Time.fixedDeltaTime);
//            }
//        }
//        else
//        {
//            stateMachine.ChangeState(player.fallingState); // Переход в состояние падения после рывка
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();

//        player.rb.gravityScale = player.gravityDef;  // Восстанавливаем гравитацию
//        player.rb.velocity = Vector2.zero;           // Останавливаем персонажа
//        player.StartCoroutine(player.DashCooldown()); // Запускаем перезарядку рывка
//        player.lockDash = false;
//    }
//}
