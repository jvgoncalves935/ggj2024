using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BotaoMenuPausa : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
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

    public void OnSelect(BaseEventData data)
    {
        SelecionarBotao();
    }

    public void OnPointerExit(PointerEventData eventData) {
        DeselecionarBotao();
    }

    public void OnDeselect(BaseEventData data){
        DeselecionarBotao();
    }

    public void DesativarBotao(){
        botaoOriginal.interactable = false;
        texto.color = new Color(1,1,1);
        ativado = false;
    }

    public void SelecionarBotao(){
        
        texto.color = new Color(1, 0, 0, 1);
        botaoOriginal.Select();
        
    }

    public void DeselecionarBotao() {
        texto.color = new Color(1, 1, 1, 1);
    }

    public bool IsAtivado(){
        return botaoOriginal.interactable;
    }

    public void Toggle(bool flag){
        ativado = flag;
    }
}
