using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem{
    private static string PATH = Application.persistentDataPath+"/Save";
    private static string PATH_FILE = PATH+"/data.bin";
    private static SaveSystem _instanciaSaveSystem;
    private static SaveData _instanciaSaveData;
    public static SaveSystem InstanciaSaveSystem{
        get{
            if( _instanciaSaveSystem == null){
                 _instanciaSaveSystem = new SaveSystem();
            }
            return  _instanciaSaveSystem;
        }
    }
    public static SaveData InstanciaSaveData{
        get{
            if( _instanciaSaveData == null){
                 _instanciaSaveData = CarregarData(); 
            }
            return  _instanciaSaveData;
        }
        set{
            _instanciaSaveData = value;
        }
    }

    public static void SalvarData(SaveData _saveData) {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        SalvarDataWIN(_saveData);
    #endif
    }

    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
    public static void SalvarDataWIN(SaveData _saveData){
        BinaryFormatter formatter = new BinaryFormatter();

        if(!Directory.Exists(PATH)){
            Directory.CreateDirectory(PATH);
        }

        InstanciaSaveData = _saveData;
        
        FileStream stream = new FileStream(PATH_FILE,FileMode.Create);
        formatter.Serialize(stream,InstanciaSaveData);
        stream.Close();
    }
    #endif

    public static SaveData CarregarData() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        CarregarDataWIN();
    #endif
        return InstanciaSaveData;
    }

    public static void CarregarDataWIN(){
        if(File.Exists(PATH_FILE)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PATH_FILE,FileMode.Open);

            try {
                InstanciaSaveData = formatter.Deserialize(stream) as SaveData;
                
            } catch(Exception ex) {
                Debug.Log("Erro ao carregar Save, criando um novo arquivo.");
                Debug.LogException(ex);
                stream.Close();
                CriarSaveVazio();
                SaveUIController.InstanciaSaveUIController.MensagemSaveCorrompido();
                SaveUIController.InstanciaSaveUIController.MensagemNovoSave();
            }
            if(stream !=null) {
                stream.Close();
            }

        } else{
            Debug.Log("Save não encontrado.");
            CriarSaveVazio();
            SaveUIController.InstanciaSaveUIController.MensagemNovoSave();
        }
        
    }

    public static void CriarSaveVazio(){
        int i;
        bool[] _conquistas = new bool[ConquistaController.NUM_CONQUISTAS];
        for(i=0;i<ConquistaController.NUM_CONQUISTAS;i++){
            _conquistas[i]=false;
        }

        bool[] _finais = new bool[ConquistaController.NUM_FINAIS];
        for(i = 0;i < ConquistaController.NUM_FINAIS;i++) {
            _finais[i] = false;
        }

        SaveData novoSave = new SaveData(false,false,_conquistas,0,0,0.0f,"en_US","data.bin",_finais,false,false,false);
        SalvarData(novoSave);
    }

    public static void SalvarAlteraçõesSave(SaveData _saveData, bool exibirMensagem){
        CarregarData();
        SalvarData(_saveData);
        if(exibirMensagem) {
            SaveUIController.InstanciaSaveUIController.MensagemSalvando();
        }
    }
}
