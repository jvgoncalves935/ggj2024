using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System.Runtime.InteropServices;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    [Header("Botões Menu Pausa")]
    [SerializeField] private Button botaoContinuar;
    [SerializeField] private Button botaoMenuPrincipal;
    [SerializeField] private Button botaoSair;
    [SerializeField] private TMP_Text textoContinuar;
    [SerializeField] private TMP_Text textoMenuPrincipal;
    [SerializeField] private TMP_Text textoSair;

    [Header("Botões UI")]
    [SerializeField] private GameObject canvas;
	public static bool jogoPausado;
	private int larguraTela;
	private int alturaTela;
	
    [SerializeField] private GameObject camadaPreta;
	private Image imagemCamadaPreta;
    [SerializeField] private Image imagemCamadaPretaFadeOut;

	private float corPretaTelaPausa = 220.0f/256.0f;
	private Dictionary<string,string> stringsMenuPausa;
    private Dictionary<string, string> stringsPersonagensPausa;
    
	private Text textoSelecionado;


    [SerializeField] public static GameObject instanciaMenuPausa;
	private static MenuPausa _instanciaMenuPausa;
	public static MenuPausa InstanciaMenuPausa{
        get{
            if( _instanciaMenuPausa == null){
                 _instanciaMenuPausa = instanciaMenuPausa.GetComponent<MenuPausa>();
            }
            return  _instanciaMenuPausa;
        }
    }
    void Awake(){
        instanciaMenuPausa = FindObjectOfType<MenuPausa>().gameObject;
    }

	void Start () {
		
		imagemCamadaPreta = camadaPreta.GetComponent<Image>();

		ToggleElementosMenuPausa(false);
		jogoPausado = false;

        CarregarStrings();
        IniciarStringsMenuPausa();
        IniciarListenersBotoes();
    }
	
	void Update () {
		VerificarBotaoPausa();
	}

	public void VerificarBotaoPausa(){
		if(Input.GetButtonDown("Pause")){

            if(jogoPausado){
                ContinuarJogo();
            }else{
                PausarJogo();
            }
		}
	}

	public void ContinuarJogo(){
		
        ToggleElementosMenuPausa(false);
        
        Time.timeScale = 1f;
        SkippableCutscenes.InstanciaSkippableCutscenes.SetInputSkip(false);
        //AudioListener.pause = false;
        jogoPausado = false;
        
        TravarCursor();
        AudioManager.InstanciaAudioManager.DespausarMusicaMenuPausa();
        AudioManager.InstanciaAudioManager.PlaySons();
        BotaoController.InstanciaBotaoController.DeselecionarBotaoMouseEnter();
    }

	public void TravarCursor(){
        MouseOperations.TravarCursorMultiPlat();
        MouseOperations.SetPosicaoCursorMultiPlat();
        MouseOperations.SalvarPosicaoCursorMultiPlat();
    }

	public void DestravarCursor(){
        MouseOperations.DestravarCursorMultiPlat();
        //mouseLook.UpdateCursorLock();
    }

    public void PausarJogo(){

        imagemCamadaPreta.color = new Color(0,0,0,corPretaTelaPausa);
        
		ToggleElementosMenuPausa(true);
		
		Time.timeScale = 0f;
        //AudioListener.pause = true;

        AudioManager.InstanciaAudioManager.PausarSons();
        jogoPausado = true;
        
		DestravarCursor();
        AudioManager.InstanciaAudioManager.PausarMusicaMenuPausa();
		
        
    }

    public void CarregarMenuPrincipal(){
        StartCoroutine(CarregarMenuPrincipalCoroutine());
	}

    public IEnumerator CarregarMenuPrincipalCoroutine(){
        
        StartCoroutine(FadeOut());
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        //AudioManager.InstanciaAudioManager.LimparSonsList();
        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        GerenciadorCena.CarregarCena("Loading");
    }

	public void ToggleElementosMenuPausa(bool valor){
        canvas.SetActive(valor);
		camadaPreta.SetActive(valor);
	}

    private void IniciarListenersBotoes() {
        botaoContinuar.onClick.AddListener(OnButtonContinuarClick);
        botaoMenuPrincipal.onClick.AddListener(OnButtonMenuPrincipalClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
    }

    private void OnButtonContinuarClick() {
        ContinuarJogo();
    }

    private void OnButtonMenuPrincipalClick() {
        CarregarMenuPrincipal();
    }

    private void OnButtonSairClick() {
        FecharJogo();
    }

    private void FecharJogo() {
        Application.Quit();
    }

    public void IniciarStringsMenuPausa(){
        textoContinuar.text = stringsMenuPausa["MENU_PAUSA_CONTINUAR"];
        textoMenuPrincipal.text = stringsMenuPausa["MENU_PAUSA_VOLTAR_MENU"];
        textoSair.text = stringsMenuPausa["MENU_PAUSA_SAIR"];

        //textsMenuPausa[4].text = eventHorizonController.stringsEventHorizon["EVT_HZN_PAUSA_OBJETIVOS"];

        /*
        Dictionary<string,string> stringsCommon = LocalizationSystem.GetDicionarioStringsCenaCommon("EventHorizonCommon");
        foreach(KeyValuePair<string, string> entrada in stringsCommon) {
            eventHorizonController.stringsEventHorizon.Add(entrada.Key, entrada.Value);
        }
        */

    }

	private Vector2 PosicaoBotaoAleatoria(){
		int x = Random.Range(0,(int)(larguraTela/3));
		int y = Random.Range(0,(int)(alturaTela/3));
		
		int[] magnitude = new int[] {1,-1};
		x = x*magnitude[Random.Range(0,2)];
		y = y*magnitude[Random.Range(0,2)];
		return new Vector2(x,y);
	}

	public void SelecionarBotaoMouseEnter(GameObject gameObject){
		EventSystem.current.SetSelectedGameObject(gameObject, null);
	}

	public void SelecionarTexto(Text novoTexto){
        if(!Object.ReferenceEquals(textoSelecionado, null)){
            textoSelecionado.color = new Color(1,1,1);
        }
        
        novoTexto.color = new Color(1,0,0);
        textoSelecionado = novoTexto;
    }

	public void DeselecionarTexto(Text novoTexto){
        textoSelecionado.color = new Color(1,1,1);
    }

    public IEnumerator FadeOut() {
        float tempo;
        for(tempo = 0.0f;tempo <= 1.0f;tempo += Time.unscaledDeltaTime) {
            imagemCamadaPretaFadeOut.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), tempo / 1.0f);
            yield return null;
        }
        imagemCamadaPretaFadeOut.color = new Color(0, 0, 0, 1);
    }

    public void SetColorTelaPreta(float valor){
        imagemCamadaPreta.color = new Color(0, 0, 0, valor);
    }

    public bool JogoPausado(){
        return jogoPausado;
    }

    private void CliqueMouseDelayMultiPlat() {
        StartCoroutine(CliqueMouseDelayMultiPlatCoroutine());
    }

    private IEnumerator CliqueMouseDelayMultiPlatCoroutine() {
        yield return new WaitForEndOfFrame();
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            MouseOperations.FocarMouseMultiPlat();
        #endif
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena("MenuPausa", out stringsMenuPausa, out stringsPersonagensPausa);
    }


}
