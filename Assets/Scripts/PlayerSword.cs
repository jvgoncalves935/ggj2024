using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            Attack(damageable);
        }
    }

    private void Attack(IDamageable damageable) {
        damageable.Kill();
    }
}
