using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : MonoBehaviour
{
    public EnemyAI Root;

    public void PunchPlayer()
    {
        PlayerController.Player.Damage(Random.Range(1, 10), Root);
    }
}