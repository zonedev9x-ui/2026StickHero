using UnityEngine;

public enum EnemyState
{
    Idle,
    Attack,
    Dead
}

public class Enemy : Character
{
    public EnemyType enemyType = EnemyType.None;

    public EnemyState currentState = EnemyState.Idle;


}
