using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Button botaoJogar;
    [SerializeField] private Button botaoCreditos;
    [SerializeField] private Button botaoOpcoes;
    [SerializeField] private Button botaoSair;
    [SerializeField] private Button botaoVoltar;

    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuOpcoes;
    // Start is called before the first frame update
    void Start()
    {
        IniciarListenersBotoes();
        VoltarMenuPrincipal();
        DesfocarMouse();
        VerificarSceneLoaderInstanciado();
        MusicaInicio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DesfocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.DestravarCursorMultiPlat();
    #endif
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Flying Circus");
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

    private void OnButtonJogarClick()
    {
        CarregarCena();
    }

    private void OnButtonCreditosClick()
    {
        CarregarCreditos();
    }

    private void OnButtonOpcoesClick() {
        CarregarOpcoes();
    }

    private void OnButtonSairClick()
    {
        FecharJogo();
    }

    private void OnButtonVoltarClick()
    {
        VoltarMenuPrincipal();
    }

    private void CarregarCreditos() {
        SceneManager.LoadScene("Scenes/Creditos", LoadSceneMode.Single);
    }

    private void CarregarCena() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("CutsceneInicial");
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
        GerenciadorCena.CarregarCena("Loading");
    }

    private void CarregarOpcoes()
    {
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(true);
    }

    private void VoltarMenuPrincipal()
    {
        menuPrincipal.SetActive(true);
        menuOpcoes.SetActive(false);
    }

    private void FecharJogo()
    {
        Application.Quit();
    }

    private void IniciarListenersBotoes()
    {
        botaoJogar.onClick.AddListener(OnButtonJogarClick);
        botaoOpcoes.onClick.AddListener(OnButtonOpcoesClick);
        botaoCreditos.onClick.AddListener(OnButtonCreditosClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
        botaoVoltar.onClick.AddListener(OnButtonVoltarClick);
    }


}
