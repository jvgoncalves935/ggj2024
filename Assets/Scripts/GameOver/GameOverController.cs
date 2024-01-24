using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Button botaoSair;
    [SerializeField] private Button botaoReiniciar;

    // Start is called before the first frame update
    void Start() {
        VerificarSceneLoaderInstanciado();
        botaoSair.onClick.AddListener(OnButtonSairClick);
        botaoSair.onClick.AddListener(OnButtonReiniciarClick);
        DesfocarMouse();
        MusicaInicio();
    }

    // Update is called once per frame
    void Update() {

    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }
    private void OnButtonReiniciarClick() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        GerenciadorCena.CarregarCena("Loading");
    }
    private void OnButtonSairClick() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        GerenciadorCena.CarregarCena("Loading");
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Dori");
    }

    private void DesfocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.DestravarCursorMultiPlat();
    #endif
    }
}
