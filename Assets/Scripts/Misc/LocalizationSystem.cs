using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;
using System.Xml;

public class LocalizationSystem{
    private static LocalizationSystem _instancia;
    private static Dictionary<string,string> stringsCenaAtual;
    private static Dictionary<string, string> stringsPersonagensCenaAtual;
    private static Dictionary<int,string> linguagens = new Dictionary<int, string>{
        {0,"pt_BR"},
        {1,"en_US"}
    };

    private static string PATH_LOCALIZATION = "Localization/";
    private static string _linguagemAtual;
    public static LocalizationSystem Instancia{
        get{
            return _instancia;
        }
        set{
            _instancia = value;
        }
    }
    public static LocalizationSystem GetInstance(){
        if(_instancia == null){
            _instancia = new LocalizationSystem();
        }
        return _instancia;
    }
    public LocalizationSystem(){
        SaveData saveData = SaveSystem.CarregarData();
        _linguagemAtual = saveData.Linguagem;
    }
    public static string NomeArquivoCenaAtual(string idLinguagem, string cenaAtual){
        return PATH_LOCALIZATION+idLinguagem+"/"+cenaAtual;
    }
    public static void IniciarStringsCenaAtual(string idLinguagem, string cena){
        IniciarDicionario();
        ParserXMLStringsLinguagem(idLinguagem,cena);
    }

    public static void IniciarDicionario(){
        stringsCenaAtual = new Dictionary<string, string>();
        stringsPersonagensCenaAtual = new Dictionary<string, string>();
    }

    public static void ParserXMLStringsLinguagem(string idLinguagem, string cenaAtual){
        //Debug.Log(idLinguagem+" "+cenaAtual);
        string arquivoXMLString = ArquivoXMLString(NomeArquivoCenaAtual(idLinguagem,cenaAtual));
        XDocument languageXMLData = XDocument.Parse(arquivoXMLString);
        Linguagem linguagem = new Linguagem();
        linguagem.id = languageXMLData.Element("Linguagem").Attribute("ID").Value;
        foreach (XElement textx in languageXMLData.Element("Linguagem").Elements()){
            string chave = textx.Attribute("key").Value;
            string valor = textx.Element("translated").Value;
            XElement personagemX = textx.Element("char");
            string personagem =  (personagemX == null) ? "" : personagemX.Value;
            //Debug.Log(chave+" "+valor);
            AdicionarStringDicionario(chave,valor);
            AdicionarStringPersonagemDicionario(chave, personagem);
        }
    }

    public static void AdicionarStringDicionario(string chave, string valor){
        stringsCenaAtual.Add(chave, valor);
    }

    public static void AdicionarStringPersonagemDicionario(string chave, string valor) {
        stringsPersonagensCenaAtual.Add(chave, valor);
    }

    public static void IniciarInstanciaDictSaveData() {
        ApagarDicionario();
        GetInstance();
        SaveData saveData = SaveSystem.CarregarData();
    }

    public static string GetString(string chave, string cena){
        GetInstance();
        SaveData saveData = SaveSystem.CarregarData();
        InstanciarDicionario(cena, saveData.Linguagem);
        return stringsCenaAtual[chave];
    }

    public static string GetPersonagemString(string chave, string cena) {
        GetInstance();
        SaveData saveData = SaveSystem.CarregarData();
        InstanciarDicionario(cena, saveData.Linguagem);
        return stringsPersonagensCenaAtual[chave];
    }

    public static Dictionary<string,string> GetDicionarioStringsCena(string cena){
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, GetLinguagem());
        return stringsCenaAtual;
    }

    public static Dictionary<string, string> GetDicionarioStringsPersonagensCena(string cena) {
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, GetLinguagem());
        return stringsPersonagensCenaAtual;
    }

    public static Dictionary<string, string> GetDicionarioStringsCena(string cena, string linguagem) {
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, linguagem);
        return stringsCenaAtual;
    }

    public static Dictionary<string, string> GetDicionarioStringsPersonagensCena(string cena, string linguagem) {
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, linguagem);
        return stringsPersonagensCenaAtual;
    }

    public static void GetDicionarioStringsFullCena(string cena, out Dictionary<string,string> dictStrings, out Dictionary<string, string> dictPersonagens) {
        dictStrings = GetDicionarioStringsCena(cena);
        dictPersonagens = GetDicionarioStringsPersonagensCena(cena);
    }

    public static void GetDicionarioStringsFullCena(string cena, string linguagem, out Dictionary<string, string> dictStrings, out Dictionary<string, string> dictPersonagens) {
        dictStrings = GetDicionarioStringsCena(cena,linguagem);
        dictPersonagens = GetDicionarioStringsPersonagensCena(cena,linguagem);
    }

    public static Dictionary<string, string> GetDicionarioStringsCenaCommon(string cena) {
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, "common");
        return stringsCenaAtual;
    }

    public static Dictionary<string, string> GetDicionarioStringsPersonagemCenaCommon(string cena) {
        IniciarInstanciaDictSaveData();
        InstanciarDicionario(cena, "common");
        return stringsPersonagensCenaAtual;
    }

    public static void GetDicionarioStringsFullCenaCommon(string cena, out Dictionary<string, string> dictStrings, out Dictionary<string, string> dictPersonagens) {
        dictStrings = GetDicionarioStringsCenaCommon(cena);
        dictPersonagens = GetDicionarioStringsPersonagemCenaCommon(cena);
    }

    public static void InstanciarDicionario(string cena, string linguagem){
        if(!IsDicionarioIniciado()){
            IniciarStringsCenaAtual(linguagem,cena);
        }
    }

    public static void ApagarDicionario(){
        stringsCenaAtual = null;
        stringsPersonagensCenaAtual = null;
    }

    public static bool IsDicionarioIniciado(){
        return stringsCenaAtual != null;
    }

    public static string ArquivoXMLString(string path){
        //Debug.Log(path);
        TextAsset textAsset = (TextAsset) Resources.Load(path);  
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        StringWriter sw = new StringWriter();
        XmlTextWriter tx = new XmlTextWriter(sw);
        doc.WriteTo(tx);
        
        return sw.ToString();
    }

    public static string TrocarBarraPath(string path){
        return path.Replace("/","\\");
    }
    public static Dictionary<int,string> DicionarioLinguagens(){
        return linguagens;
    }

    public static string LinguagemAtual(){
        return _linguagemAtual;
    }

    public static void SetLinguagem(string linguagem){
        _linguagemAtual = linguagem;
    }
    public static string GetLinguagem() {
        return _linguagemAtual;
    }

}

[System.Serializable]
public class Linguagem
{
    public string id;
    public List<StringLocalization> strings = new List<StringLocalization>();
}

[System.Serializable]
public class StringLocalization
{
    public string stringID;
    public StringLocalizationInfo infos;
}

[System.Serializable]
public class StringLocalizationInfo
{
    public string stringOriginal;
    public string stringTraduzida;
}
