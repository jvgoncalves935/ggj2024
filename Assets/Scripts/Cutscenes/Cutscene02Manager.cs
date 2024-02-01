using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene02Manager: MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text textoUI;
    private int indiceTextoAtual;
    [SerializeField] private Image[] imagensCutscene;

    private Dictionary<string, string> stringsCutscene;
    private Dictionary<string, string> stringsPersonagensCutsceneInicial;

    private static int NUM_IMAGENS = 1;

    private string[] textosCutscenes;

    [SerializeField] public static GameObject instanciaCutscene02Manager;
    private static Cutscene02Manager _instanciaCutscene02Manager;
    public static Cutscene02Manager InstanciaCutscene02Manager {
        get {
            if(_instanciaCutscene02Manager == null) {
                _instanciaCutscene02Manager = instanciaCutscene02Manager.GetComponent<Cutscene02Manager>();
            }
            return _instanciaCutscene02Manager;
        }
    }
    void Awake() {
        instanciaCutscene02Manager = FindObjectOfType<Cutscene02Manager>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        indiceTextoAtual = 0;
        StartCoroutine(CutsceneInicial());
        VerificarSceneLoaderInstanciado();
        MusicaInicio();
        IniciarCoresImagens();
        CarregarStrings();
        AplicarStrings();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkipCutscene();
    }
    private IEnumerator CutsceneInicial() {
        yield return new WaitForSeconds(0f);
    
        StartCoroutine(FadeIn(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3f));

        SetText(textosCutscenes[1]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[2]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[3]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5f));

        SetText(textosCutscenes[4]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));


        StartCoroutine(FadeOut(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);
        

        yield return new WaitForSeconds(1f);
        IniciarPrimeiraFase();
    }
    private void IniciarPrimeiraFase() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Fase03");
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
        GerenciadorCena.CarregarCena("Loading");
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
        AudioManager.InstanciaAudioManager.Play("Rogue Circus");
    }

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private IEnumerator FadeIn(Image imagem, float tempoFinal) {
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

    private void IniciarCoresImagens() {
        for(int i = 0;i < NUM_IMAGENS;i++) {
            imagensCutscene[i].color = new Color(1, 1, 1, 0);
        }
    }

    private void CheckSkipCutscene() {
        if(Input.GetButtonDown("Escape")) {
            IniciarPrimeiraFase();
        }
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsCutscene, out stringsPersonagensCutsceneInicial);
    }

    private void AplicarStrings() {
        textosCutscenes = new string[stringsCutscene.Count];
        for(int i = 0;i < stringsCutscene.Count;i++) {
            if(stringsPersonagensCutsceneInicial["CUTSCENE02_" + i] != "") {
                textosCutscenes[i] = "[" + stringsPersonagensCutsceneInicial["CUTSCENE02_" + i] + "]\n" + stringsCutscene["CUTSCENE02_" + i];
            } else {
                textosCutscenes[i] = stringsCutscene["CUTSCENE02_" + i];
            }

        }
    }
}
