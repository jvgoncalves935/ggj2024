using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase03Manager:MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Start() {
        CarregarCenaPause();
        VerificarSceneLoaderInstanciado();
    }

    // Update is called once per frame
    void Update() {

    }

    private void CarregarCenaPause() {
        int countLoaded = SceneManager.sceneCount;
        bool cenaMenuPausaLoaded = false;
        Scene[] loadedScenes = new Scene[countLoaded];

        for(int i = 0;i < countLoaded;i++) {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
            if(loadedScenes[i].name == "MenuPausa") {
                cenaMenuPausaLoaded = true;
                break;
            }
        }

        if(!cenaMenuPausaLoaded) {
            SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
        }
    }

    private void DescarregarCenaPausa() {
        SceneManager.UnloadSceneAsync("MenuPausa");
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

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Lenda do Espírito");
    }


}

