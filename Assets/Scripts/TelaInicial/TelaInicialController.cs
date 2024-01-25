using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaInicialController : MonoBehaviour{
    [SerializeField] private Image imagemOuroboros;
    [SerializeField] private Image imagemForceDev;
    [SerializeField] private Image imagemCMS;
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Text textTelaInicial;
    public Dictionary<string, string> stringsTelaInicial;
    private string mensagem;
    private string bufferMensagem;
    private int tamanhoMensagem;
    private int tamanhoBufferMensagem;

    private void Awake() {
        
    }

    void Start()
    {
        VerificarScenesDataInstanciado();
        //FocarMouse();
        VerificarVideoPreload();
        MusicaInicio();
    }

    private IEnumerator IniciarCutscene(){
        
        StartCoroutine(FadeIn(imagemOuroboros,0.7f));
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(FadeOut(imagemOuroboros,0.7f));
        yield return new WaitForSeconds(0.9f);
        yield return new WaitForSeconds(0.8f);


        StartCoroutine(FadeIn(imagemForceDev, 0.7f));
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(FadeOut(imagemForceDev, 0.7f));
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.9f);
        
        StartCoroutine(FadeIn(imagemCMS, 0.8f));
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeOut(imagemCMS, 0.8f));
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(1.0f);

        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        GerenciadorCena.CarregarCena("Loading");
    }

    private IEnumerator FadeIn(Image imagem, float tempoFinal){
        float tempo;
        for(tempo = 0.0f;tempo <= tempoFinal;tempo += Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator FadeOut(Image imagem, float tempoFinal) {
        float tempo;
        for(tempo = tempoFinal;tempo >= 0.0f;tempo -= Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 0);
    }

    public void VerificarScenesDataInstanciado() {
        if(FindObjectOfType<ScenesData>() == null) {
            Instantiate(scenesData);
            scenesData = ScenesData.InstanciaScenesData;

            Instantiate(inputNames);
            inputNames = InputNames.InstanciaInputNames;

            Instantiate(sceneLoader);
            sceneLoader = SceneLoader.InstanciaSceneLoader;

            Instantiate(audioManager);
            Debug.Log("SceneData criado em TelaInicial");
        } else {
            Debug.Log("SceneData anteriormente criado");
        }
    }

    private void FocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            MouseOperations.FocarMouseMultiPlat();
    #endif
    }

    private void VerificarVideoPreload() {
        if(!ConquistaController.GetVideoPreloadAtivado()) {
            StartCoroutine(IniciarCutscene());
        } else {
            StartCoroutine(IniciarVideoPreload());
        }
    }

    private IEnumerator IniciarVideoPreload() {
        IniciarMensagem();
        while(tamanhoBufferMensagem < tamanhoMensagem) {
            AvancarMensagem();
            yield return new WaitForSeconds(Random.Range(0.05f,0.15f));
        }
        yield return new WaitForSeconds(3f);

        ConquistaController.DesativarVideoPreload();
        FecharJogo();
    }

    private void IniciarMensagem() {
        stringsTelaInicial = LocalizationSystem.GetDicionarioStringsCena("TelaInicial");
        mensagem = stringsTelaInicial["TELA_INICIAL_MENSAGEM"];
        tamanhoMensagem = mensagem.Length;
        bufferMensagem = "";
        tamanhoBufferMensagem = 0;
    }

    private void AvancarMensagem() {
        bufferMensagem += mensagem[tamanhoBufferMensagem];
        tamanhoBufferMensagem += 1;
        textTelaInicial.text = bufferMensagem;
    }

    private void FecharJogo() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            FecharJogoWIN();
    #endif
    }

    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
    private void FecharJogoWIN() {
        Application.Quit();
    }
    #endif

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
        AudioManager.InstanciaAudioManager.Play("R.E.");
    }

}
