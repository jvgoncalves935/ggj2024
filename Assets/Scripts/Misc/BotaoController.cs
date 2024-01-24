using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoController : MonoBehaviour
{
    private List<Botao> listaBotao;

    [SerializeField] public static GameObject instanciaBotaoController;
    private static BotaoController _instanciaBotaoController;
    public static BotaoController InstanciaBotaoController {
        get {
            if(_instanciaBotaoController == null) {
                _instanciaBotaoController = instanciaBotaoController.GetComponent<BotaoController>();
            }
            return _instanciaBotaoController;
        }
    }
    void Awake() {
        instanciaBotaoController = FindObjectOfType<BotaoController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        listaBotao = GetAllObjectsBotaoOnlyInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Botao> GetAllObjectsBotaoOnlyInScene() {
        List<Botao> objectsInScene = new List<Botao>();

        foreach(Botao botao in Resources.FindObjectsOfTypeAll(typeof(Botao)) as Botao[]) {
            //if(!UnityEditor.EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            objectsInScene.Add(botao);
        }

        return objectsInScene;
    }

    public void DeselecionarBotaoMouseEnter() {
        for(int i = 0;i < listaBotao.Count;i++) {
            listaBotao[i].DeselecionarBotao();
        }
    }
}
