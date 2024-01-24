using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private Enemy enemyParent;
    private int damage;

    private void Start() {
        enemyParent = transform.parent.GetComponent<Enemy>();
        damage = enemyParent.GetDamage();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && !PlayerController.InstanciaPlayerController.IsPlayerHit()) {
            enemyParent.DamagePlayer(damage);
        }
    }
}
