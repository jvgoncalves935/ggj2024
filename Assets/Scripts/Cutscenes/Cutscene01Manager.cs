using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene01Manager: MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text textoUI;
    private int indiceTextoAtual;
    [SerializeField] private Image[] imagensCutscene;

    private Dictionary<string, string> stringsCutsceneInicial;
    private Dictionary<string, string> stringsPersonagensCutsceneInicial;

    private static int NUM_IMAGENS = 3;

    private string[] textosCutscenes;

    [SerializeField] public static GameObject instanciaCutscene01Manager;
    private static Cutscene01Manager _instanciaCutscene01Manager;
    public static Cutscene01Manager InstanciaCutscene01Manager {
        get {
            if(_instanciaCutscene01Manager == null) {
                _instanciaCutscene01Manager = instanciaCutscene01Manager.GetComponent<Cutscene01Manager>();
            }
            return _instanciaCutscene01Manager;
        }
    }
    void Awake() {
        instanciaCutscene01Manager = FindObjectOfType<Cutscene01Manager>().gameObject;
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

        //Imagem 1
        StartCoroutine(FadeIn(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);
        
        SetText(textosCutscenes[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[1]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[2]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[3]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        

        StartCoroutine(FadeOut(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagem 2
        StartCoroutine(FadeIn(imagensCutscene[1], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[4]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[5]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[6]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[7]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4.5f));

        StartCoroutine(FadeOut(imagensCutscene[1], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);


        //Imagem 3
        StartCoroutine(FadeIn(imagensCutscene[2], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[8]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[9]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5.5f));

        SetText(textosCutscenes[10]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.0f));

        SetText(textosCutscenes[11]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.0f));

        StartCoroutine(FadeOut(imagensCutscene[2], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        yield return new WaitForSeconds(1f);
        IniciarPrimeiraFase();
    }
    private void IniciarPrimeiraFase() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Fase02");
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
        AudioManager.InstanciaAudioManager.Play("Close Encounters");
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
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsCutsceneInicial, out stringsPersonagensCutsceneInicial);
    }

    private void AplicarStrings() {
        textosCutscenes = new string[12];
        for(int i = 0;i < 12;i++) {
            textosCutscenes[i] = stringsCutsceneInicial["CUTSCENE01_" + i];
        }
    }
}
