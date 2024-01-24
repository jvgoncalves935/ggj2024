using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyStats
{
    protected bool killed = false;
    protected int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void DamagePlayer(int enemyDamage) {
        if(!killed) {
            PlayerController.InstanciaPlayerController.PlayerDamage(enemyDamage);
        }
    }

    public virtual void Kill() {

    }

    public virtual void Restart() {

    }

    public int GetDamage() {
        return damage;
    }

    public virtual void SetBaseDamage(int baseDamage) {

    }



}
