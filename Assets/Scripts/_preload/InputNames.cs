using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputNames : MonoBehaviour
{
    private static Dictionary<string, string> nomesInputs;

    [SerializeField] private static InputNames _instanciaInputNames;
    [SerializeField] public static GameObject instanciaInputNames;
    public static InputNames InstanciaInputNames {
        get {
            if(_instanciaInputNames == null) {
                _instanciaInputNames = instanciaInputNames.GetComponent<InputNames>();
            }
            return _instanciaInputNames;
        }
    }

    private void Awake() {
        instanciaInputNames = FindObjectOfType<InputNames>().gameObject;
        DontDestroyOnLoad(instanciaInputNames);
        IniciarInputAxis();
    }

    public Dictionary<string,string> IniciarInputAxis() {
        nomesInputs = new Dictionary<string, string>();

        /*
        #if UNITY_EDITOR
            var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);

            SerializedProperty iterator = serializedObject.FindProperty("m_Axes");

            string nomeAtual = "";
            while(iterator.Next(true)){
                SerializedProperty copia = iterator.Copy();
                if(copia.name == "m_Name"){
                    nomeAtual = copia.stringValue;
                }

                if(copia.name == "positiveButton"){
                    nomesInputs.Add(nomeAtual, copia.stringValue.ToUpper());
                }
            }
        #endif
        */

        
        nomesInputs.Add("Horizontal","RIGHT");
        nomesInputs.Add("Vertical", "UP");
        nomesInputs.Add("Fire1", "LEFT CTRL");
        nomesInputs.Add("Fire2", "LEFT ALT");
        nomesInputs.Add("Fire3", "LEFT SHIFT");
        nomesInputs.Add("Jump", "SPACE");
        nomesInputs.Add("Mouse X", "MOUSE");
        nomesInputs.Add("Mouse Y", "MOUSE");
        nomesInputs.Add("Mouse ScrollWheel", "MOUSE SCRL");
        nomesInputs.Add("Submit", "ENTER");
        nomesInputs.Add("Cancel", "ESC");
        nomesInputs.Add("Use", "F");
        nomesInputs.Add("Lanterna", "I");
        nomesInputs.Add("Pause", "ESC");
        
        return nomesInputs;
    }

    public string GetInputName(string chave){

        if(!DicionarioIniciado()) {
            return null;
        }
        if(nomesInputs.ContainsKey(chave)) {
            return nomesInputs[chave];
        }
        return null;
        
    }

    public static bool DicionarioIniciado() {
        return nomesInputs != null;
    }

}
