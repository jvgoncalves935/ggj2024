using UnityEngine;

public class PreloadController : MonoBehaviour
{
    void Start(){
        if(GerenciadorCena.NomeCenaAtual() != "_preload"){
            Destroy(gameObject);
            return;
        }
        SceneLoader.InstanciaSceneLoader.SetProximaCena("TelaInicial");
        GerenciadorCena.CarregarCena("TelaInicial");
    }
}
