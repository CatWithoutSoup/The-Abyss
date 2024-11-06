//using UnityEngine;


//public class JumpingState : PlayerState
//{
//    private float jumpForce = 100f; // Примерная сила прыжка
//    private int jumpValueIter = 60; // Общее число итераций прыжка
//    private int jumpIter = 0; // Текущий счётчик итераций

//    public JumpingState(PlayerMovement player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

//    public override void Enter()
//    {
//        base.Enter();
//        player.rb.velocity = new Vector2(player.rb.velocity.x, 0); // Обнуление вертикальной скорости перед прыжком
//        jumpIter = 0; // Сброс итератора прыжка
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();

//        // Проверка на NaN и Infinity перед добавлением силы
//        if (jumpIter < jumpValueIter)
//        {
//            float force = jumpForce / Mathf.Max(1f, jumpIter / 3f); // Предотвращение деления на ноль или слишком малые значения
//            if (!float.IsNaN(force) && !float.IsInfinity(force))
//            {
//                player.rb.AddForce(Vector2.up * force);
//                jumpIter++;
//            }
//            else
//            {
//                Debug.LogWarning("Сила прыжка стала некорректной: " + force);
//            }
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        jumpIter = 0; // Сброс итератора прыжка при выходе из состояния
//    }
//}
