using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausaManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private bool ativado;

    [SerializeField] private Button botaoContinuar;
    [SerializeField] private Button botaoMenuPrincipal;
    [SerializeField] private Button botaoSair;
    [SerializeField] private GameObject camadaPreta;

    private Camera cameraMain;
    private float corPretaTelaPausa = 220.0f / 256.0f;
    private Image imagemCamadaPreta;
    private BotaoMenuPausa botaoContinuarMP;

    [SerializeField] public static GameObject instanciaMenuPausaManager;
    private static MenuPausaManager _instanciaMenuPausaManager;
    public static MenuPausaManager InstanciaMenuPausaManager
    {
        get
        {
            if (_instanciaMenuPausaManager == null)
            {
                _instanciaMenuPausaManager = instanciaMenuPausaManager.GetComponent<MenuPausaManager>();
            }
            return _instanciaMenuPausaManager;
        }
    }

    private void Awake()
    {
        instanciaMenuPausaManager = FindObjectOfType<MenuPausaManager>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        IniciarMenuPausa();
        IniciarListenersBotoes();
    }

    // Update is called once per frame
    void Update()
    {
        GetButtonPausa();
    }

    private void GetButtonPausa()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!ativado)
            {
                PausarJogo();
            }
            else
            {
                ContinuarJogo();
            }
            
        }
    }

    private void ToggleMenuPausa(bool flag)
    {
        ativado = flag;
        canvas.SetActive(flag);
    }

    private void IniciarMenuPausa()
    {
        cameraMain = Camera.main;
        ativado = false;
        canvas.SetActive(ativado);
        imagemCamadaPreta = camadaPreta.GetComponent<Image>();
        imagemCamadaPreta.color = new Color(0, 0, 0, corPretaTelaPausa);
    }

    private void IniciarListenersBotoes()
    {
        botaoContinuar.onClick.AddListener(OnButtonContinuarClick);
        botaoMenuPrincipal.onClick.AddListener(OnButtonMenuPrincipalClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
        botaoContinuarMP = botaoContinuar.GetComponent<BotaoMenuPausa>();
    }



    public static void TravarCursor()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void DestravarCursor()
    {
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnButtonContinuarClick()
    {
        ContinuarJogo();
    }

    private void OnButtonMenuPrincipalClick()
    {
        CarregarMenuPrincipal();
    }

    private void OnButtonSairClick()
    {
        FecharJogo();
    }

    private void FecharJogo()
    {
        Application.Quit();
    }

    private void ContinuarJogo()
    {
        StartCoroutine(ContinuarJogoCoroutine());
    }

    public void PausarJogo()
    {
        ToggleMenuPausa(true);
        Time.timeScale = 0f;
        DestravarCursor();
        //botaoContinuarMP.SelecionarBotao();

    }

    public void CarregarMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MenuPrincipal", LoadSceneMode.Single);
    }


    

    private IEnumerator ContinuarJogoCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        ToggleMenuPausa(false);
        Time.timeScale = 1f;
        TravarCursor();
    }

}
