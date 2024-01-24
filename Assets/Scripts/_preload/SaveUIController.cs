using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveUIController : MonoBehaviour
{
    [SerializeField] public static GameObject instanciaSaveUIController;
    private static SaveUIController _instanciaSaveUIController;
    private Dictionary<string, string> stringsSaveUI;
    private List<Tuple<string,float>> stringsAtual;
    private SaveData saveData;
    [SerializeField] private GameObject objUI;
    [SerializeField] private TMP_Text textMensagem;
    //[SerializeField] private GameObject objUI;
    private bool dicionarioCarregado;
    public static SaveUIController InstanciaSaveUIController {
        get {
            if(_instanciaSaveUIController == null) {
                _instanciaSaveUIController = instanciaSaveUIController.GetComponent<SaveUIController>();
            }
            return _instanciaSaveUIController;
        }
    }
    void Awake() {
        instanciaSaveUIController = FindObjectOfType<SaveUIController>().gameObject;
        stringsAtual = new List<Tuple<string, float>>();
        dicionarioCarregado = false;
        ReiniciarDicionario(LocalizationSystem.GetLinguagem());
        DontDestroyOnLoad(instanciaSaveUIController);
    }

    void Start() {
        
        
        objUI.SetActive(false);
        IniciarUI();
        
    }
    public void MensagemSalvando() {
        AdicionarMensagem(stringsSaveUI["SAVE_UI_SALVANDO"],1.0f);
    }

    public void MensagemSaveCorrompido() {
        AdicionarMensagem(stringsSaveUI["SAVE_UI_SAVE_CORROMPIDO"],5.0f);
    }

    public void MensagemNovoSave() {
        AdicionarMensagem(stringsSaveUI["SAVE_UI_NOVO_SAVE"],3.0f);
    }

    public void IniciarDicionario() {
        stringsSaveUI = new Dictionary<string, string>() {
            { "SAVE_UI_NOVO_SAVE", "New Save File created." },
            { "SAVE_UI_SAVE_CORROMPIDO", "Error: Save File corrupted, creating new file." },
            { "SAVE_UI_SALVANDO", "Saving..." }
        };
    }

    private void AdicionarMensagem(string mensagem, float tempo) {
        if(IsCenaBloqueada()) {
            return;
        }
        Debug.Log(mensagem + " "+tempo);
        Tuple<string, float> tupla = new Tuple<string, float>(mensagem, tempo);
        stringsAtual.Add(tupla);
    }

    private void ApagarMensagem() {
        stringsAtual.RemoveAt(0);
    }

    private void ExibirMensagem() {
        StartCoroutine(ExibirMensagemCoroutine());
    }

    private IEnumerator ExibirMensagemCoroutine() {
        textMensagem.text = stringsAtual[0].Item1;
        //objUI.SetActive(true);
        yield return new WaitForSeconds(stringsAtual[0].Item2);
        //objUI.SetActive(false);
        textMensagem.text = "";
        ApagarMensagem();
    }

    private void IniciarUI() {
        StartCoroutine(UICoroutine());
    }
    private IEnumerator UICoroutine() {
        while(true) {
            if(stringsAtual.Count > 0) {
                ExibirMensagem();
                yield return new WaitForSeconds(stringsAtual[0].Item2);
            } else {
                yield return new WaitForSeconds(1.0f);
            }
            
        }
    }

    public void ReiniciarDicionario(string linguagem) {
        if(!dicionarioCarregado) {
            IniciarDicionario();
            dicionarioCarregado = true;
            return;
        }
        stringsSaveUI = LocalizationSystem.GetDicionarioStringsCena("SaveUI",linguagem);
    }

    private bool IsCenaBloqueada() {
        string cenaAtual = GerenciadorCena.NomeCenaAtual();
        if(cenaAtual == "_preload" || cenaAtual ==  "TelaInicial") {
            return true;
        }
        return false;
    }
}
