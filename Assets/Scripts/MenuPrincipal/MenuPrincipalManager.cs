using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private Button botaoJogar;
    [SerializeField] private Button botaoCreditos;
    [SerializeField] private Button botaoSair;
    [SerializeField] private Button botaoVoltar;

    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuCreditos;
    // Start is called before the first frame update
    void Start()
    {
        IniciarListenersBotoes();
        VoltarMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonJogarClick()
    {
        CarregarCena();
    }

    private void OnButtonCreditosClick()
    {
        CarregarCreditos();
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

    private void CarregarCreditos()
    {
        menuPrincipal.SetActive(false);
        menuCreditos.SetActive(true);
    }

    private void VoltarMenuPrincipal()
    {
        menuPrincipal.SetActive(true);
        menuCreditos.SetActive(false);
    }

    private void FecharJogo()
    {
        Application.Quit();
    }

    private void IniciarListenersBotoes()
    {
        botaoJogar.onClick.AddListener(OnButtonJogarClick);
        botaoCreditos.onClick.AddListener(OnButtonCreditosClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
        botaoVoltar.onClick.AddListener(OnButtonVoltarClick);
    }


}
