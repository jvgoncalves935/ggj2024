using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AddHealthPlayer();
        }
    }

    void AddHealthPlayer()
    {
        PlayerController.InstanciaPlayerController.PlayerAddHealth(healthAmount);
        gameObject.SetActive(false);
    }
}
