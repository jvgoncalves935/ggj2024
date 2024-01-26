using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
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

    private void CarregarCena() {
        SceneManager.LoadScene("Scenes/CutsceneInicial", LoadSceneMode.Single);
    }

    private void CarregarCreditos() {
        SceneManager.LoadScene("Scenes/Creditos", LoadSceneMode.Single);
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
