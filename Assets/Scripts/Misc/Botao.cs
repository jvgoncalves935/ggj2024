using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Botao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text texto;
    [SerializeField] private bool ativado;
    public Button botaoOriginal{get;set;}
    
    void Awake()
    {
        texto = GetComponentInChildren<TMP_Text>();
        botaoOriginal = GetComponent<Button>();
        ativado = true;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        SelecionarBotao();
    }

    public void OnPointerExit(PointerEventData eventData) {
        DeselecionarBotao();
    }

    public void DesativarBotao(){
        botaoOriginal.interactable = false;
        texto.color = new Color(1,1,1);
        ativado = false;
    }

    public void SelecionarBotao(){
        //Debug.Log("tt");
        if(IsAtivado()) {
            texto.color = new Color(1, 0, 0, 1);
        }
    }

    public void DeselecionarBotao() {
        if(IsAtivado()) {
            texto.color = new Color(1, 1, 1, 1);
        }
    }

    public bool IsAtivado(){
        return botaoOriginal.interactable;
    }

    public void Toggle(bool flag){
        ativado = flag;
    }
}
