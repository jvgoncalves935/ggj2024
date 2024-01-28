using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneFinalController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    private static int NUM_IMAGENS = 8;
    [SerializeField] private Image[] imagensCutscene;
    [SerializeField] private TMP_Text textoUI;

    private Dictionary<string, string> stringsCutsceneFinal;
    private Dictionary<string, string> stringsPersonagensCutsceneFinal;

    private string[] textosCutscenes;

    [SerializeField] public static GameObject instanciaCutsceneFinalController;
    private static CutsceneFinalController _instanciaCutsceneFinalController;
    public static CutsceneFinalController InstanciaCutsceneFinalController {
        get {
            if(_instanciaCutsceneFinalController == null) {
                _instanciaCutsceneFinalController = instanciaCutsceneFinalController.GetComponent<CutsceneFinalController>();
            }
            return _instanciaCutsceneFinalController;
        }
    }
    void Awake() {
        instanciaCutsceneFinalController = FindObjectOfType<CutsceneFinalController>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        IniciarCoresImagens();
        VerificarSceneLoaderInstanciado();
        StartCoroutine(CutsceneFinal());
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

    private IEnumerator CutsceneFinal() {
        yield return new WaitForSeconds(2f);

        
        //Imagens 1
        StartCoroutine(FadeIn(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[1]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[2]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[3]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[4]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));


        StartCoroutine(FadeOut(imagensCutscene[0], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 2
        StartCoroutine(FadeIn(imagensCutscene[1], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[5]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[6]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[7]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[8]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[9]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[10]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[11]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[12]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        SetText(textosCutscenes[13]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[14]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[1], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 3
        StartCoroutine(FadeIn(imagensCutscene[2], 0.6f));
        yield return new WaitForSeconds(0.6f);

        //SetText(textosCutscenes[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        StartCoroutine(FadeOut(imagensCutscene[2], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 4
        PararMusica();

        StartCoroutine(FadeIn(imagensCutscene[3], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[15]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        StartCoroutine(FadeOut(imagensCutscene[3], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 5
        StartCoroutine(FadeIn(imagensCutscene[4], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[16]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3f));

        SetText(textosCutscenes[17]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5.5f));

        SetText(textosCutscenes[18]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5f));

        SetText(textosCutscenes[19]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        SetText(textosCutscenes[20]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

        StartCoroutine(FadeOut(imagensCutscene[4], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 6
        StartCoroutine(FadeIn(imagensCutscene[5], 0.6f));
        yield return new WaitForSeconds(0.6f);

        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[5], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 7
        StartCoroutine(FadeIn(imagensCutscene[6], 0.6f));
        yield return new WaitForSeconds(0.6f);

        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[6], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);

        //Imagens 8
        StartCoroutine(FadeIn(imagensCutscene[7], 0.6f));
        yield return new WaitForSeconds(0.6f);

        SetText(textosCutscenes[21]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[22]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[23]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[24]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[25]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        SetText(textosCutscenes[26]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

        StartCoroutine(FadeOut(imagensCutscene[7], 0.6f));
        yield return new WaitForSeconds(0.6f);
        SetText("");
        yield return new WaitForSeconds(0.6f);


        yield return new WaitForSeconds(3f);
        IniciarCenaCreditos();
    }
    private void IniciarCenaCreditos() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Creditos");
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
        AudioManager.InstanciaAudioManager.Play("Boss Extravaganza");
    }

    public void PararMusica() {
        AudioManager.InstanciaAudioManager.StopMusicaAtual();
        AudioManager.InstanciaAudioManager.Play("Final Reverie");
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

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private void CheckSkipCutscene() {
        if(Input.GetButtonDown("Escape")) {
            IniciarCenaCreditos();
        }
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsCutsceneFinal, out stringsPersonagensCutsceneFinal);
    }

    private void AplicarStrings() {
        textosCutscenes = new string[27];
        for(int i = 0;i < 27;i++) {
            textosCutscenes[i] = stringsCutsceneFinal["FINAL_" + i];
        }
    }
}
