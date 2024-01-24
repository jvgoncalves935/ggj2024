using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTrigger : MonoBehaviour
{
    private EnemyPatrol enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && !PlayerController.InstanciaPlayerController.IsPlayerHit()) {
            KillEnemy();
        }
    }

    private void KillEnemy() {
        enemy.Kill();
        enemy.gameObject.SetActive(false);
    }
}
