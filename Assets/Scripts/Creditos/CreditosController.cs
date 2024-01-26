using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditosController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Button botaoSair;

    [SerializeField] private TMP_Text[] textosUI;
    [SerializeField] private TMP_Text textoFugir;

    private Dictionary<string, string> stringsCreditos;
    private Dictionary<string, string> stringsPersonagensCreditos;

    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        botaoSair.onClick.AddListener(OnButtonSairClick);
        DesfocarMouse();
        MusicaInicio();
        CarregarStrings();
        CarregarStringsCommon();
        AplicarStrings();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnButtonSairClick() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        GerenciadorCena.CarregarCena("Loading");
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("R.E. (Full)");
    }

    private void DesfocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.DestravarCursorMultiPlat();
    #endif
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsCreditos, out stringsPersonagensCreditos);
    }

    private void CarregarStringsCommon() {
        Dictionary<string, string> stringsLinguagens = LocalizationSystem.GetDicionarioStringsCenaCommon(GerenciadorCena.NomeCenaAtual() + "Common");
        foreach(KeyValuePair<string, string> entrada in stringsLinguagens) {
            //Debug.Log(entrada.Key + " " + entrada.Value);
            stringsCreditos.Add(entrada.Key, entrada.Value);
        }
    }

    private void AplicarStrings() {
        
        for(int i = 0;i < 13;i++) {
            textosUI[i*2].text = stringsCreditos["CREDITOS_COMMON_" + i];
            textosUI[i*2+1].text = stringsCreditos["CREDITOS_" + i];
        }

        textoFugir.text = stringsCreditos["CREDITOS_FUGIR"];
    }


}
