using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelBarrier : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            IniciarProximaFase();
        }
    }

    private void IniciarProximaFase() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena(nextScene);
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
        GerenciadorCena.CarregarCena("Loading");
    }

}
