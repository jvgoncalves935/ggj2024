using System.Collections.Generic;
using UnityEngine;

public class ScenesData : MonoBehaviour{
    private static Dictionary<string,string> scenesData;
    [SerializeField] private static ScenesData _instanciaScenesData;
    [SerializeField] public static GameObject instanciaScenesData;
    public static ScenesData InstanciaScenesData{
        get{
            if( _instanciaScenesData == null){
                _instanciaScenesData = instanciaScenesData.GetComponent<ScenesData>();
                scenesData = new Dictionary<string, string>();
            }
            return  _instanciaScenesData;
        }
    }

    private void Awake(){
        instanciaScenesData = FindObjectOfType<ScenesData>().gameObject;
        DontDestroyOnLoad(instanciaScenesData);
    }

    public void SetScenesData(Dictionary<string,string> dict){
        scenesData = dict;
    }
    public void AddScenesData(string chave, string valor){
        scenesData[chave] = valor;
    }

    public Dictionary<string,string> GetScenesDataDict(){
        return scenesData;
    }

    public string GetScenesData(string chave){
        if(!DicionarioIniciado()) {
            return null;
        }
        if(scenesData.ContainsKey(chave)) {
            return scenesData[chave];
        }
        return null;
    }

    public void RemoveSceneData(string chave) {
        if(!DicionarioIniciado()){
            return;
        }
        if(scenesData.ContainsKey(chave)) {
            scenesData.Remove(chave);
        }
    }

    public void DeletarScenesData(){
        _instanciaScenesData = null;
        Destroy(gameObject);
    }

    public static bool DicionarioIniciado(){
        return scenesData != null;
    }
}
