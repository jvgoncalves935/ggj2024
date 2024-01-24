using UnityEngine;
using System;

public class ConquistaController{
    private static float _TEMPO_CONQUISTA_ENTUSIASTA_OCULTISMO = 30.0f*60.0f;
    private static float _TEMPO_CONQUISTA_ISSONÃOESTÁACONTECENDO = 10.0f*60.0f;
    private static float _TEMPO_CONQUISTA_OGRANDESEGREDO = 2.0f*60.0f;
    private static string[] _nomesConquistas = {
        "Blecaute",
        "Isso Não Está Acontecendo",
        "Quem Está Rindo Agora?",
        "Entusiasta do Ocultismo",
        "Sobrevivente I",
        "Sobrevivente II",
        "Sobrevivente III",
        "O Grande Segredo",
        "Agonia Eterna",
        "Iconoclasta",
        "Carta do Madeira",
        "Cuidado Onde Pisa...",
        "Péssima Ideia.",
        "Arte Urbana Demoníaca",
        "\"Ghosts não existem...\"",
        "VHJhbnNtaXNzw6Nv"
    };

    private static int _NUM_CONQUISTAS = _nomesConquistas.Length;
    private static int _NUM_TOTEMS_CONQUISTA = 25;
    private static int _NUM_FINAIS = 3;
    
    public static string[] NomesConquistas{
        get{
            return _nomesConquistas;
        }
    }
    public static int NUM_CONQUISTAS{
        get{
            return _NUM_CONQUISTAS;
        }
    }
    public static int NUM_FINAIS {
        get {
            return _NUM_FINAIS;
        }
    }
    public static int NUM_TOTEMS_CONQUISTA{
        get{
            return _NUM_TOTEMS_CONQUISTA;
        }
    }
    public static float TEMPO_CONQUISTA_ENTUSIASTA_OCULTISMO{
        get{
            return _TEMPO_CONQUISTA_ENTUSIASTA_OCULTISMO;
        }
    }
    public static float TEMPO_CONQUISTA_ISSONÃOESTÁACONTECENDO{
        get{
            return _TEMPO_CONQUISTA_ISSONÃOESTÁACONTECENDO;
        }
    }
    public static float TEMPO_CONQUISTA_OGRANDESEGREDO{
        get{
            return _TEMPO_CONQUISTA_OGRANDESEGREDO;
        }
    } 
    public static void SalvarCartaMadeira(){
        SaveData saveData = SaveSystem.CarregarData();
        saveData.CartaMadeira = true;
        SaveSystem.SalvarData(saveData);
    }

    public static bool IsCartaMadeiraAtivada(){
        SaveData saveData = SaveSystem.CarregarData();
        return saveData.CartaMadeira;
    }

    public static void SalvarNoticiaJornal(){
        SaveData saveData = SaveSystem.CarregarData();
        saveData.NoticiaJornal = true;
        SaveSystem.SalvarData(saveData);
    }

    public static bool IsNoticiaJornalAtivada(){
        SaveData saveData = SaveSystem.CarregarData();
        return saveData.NoticiaJornal;
    }

    public static void SalvarConquista(int idConquista){
        SaveData saveData = SaveSystem.CarregarData();
        saveData.Conquistas[idConquista] = true;
        SaveSystem.SalvarData(saveData);
    }

    public static string LogSaveData(){
        SaveData saveData = SaveSystem.CarregarData();
        string log = "";
        log += "CartaMadeira: "+saveData.CartaMadeira+"\nNoticiaJornal: "+saveData.NoticiaJornal+"\nTempo Mundo Diego: "+saveData.TempoMundoDiego+"\nTotems: "+saveData.Totems+"\nVitorias: "+saveData.Vitorias+"\n\nConquistas:\n";
        
        int i;
        for(i=0;i<NUM_CONQUISTAS;i++){
            log += "["+i+"] "+NomesConquistas[i]+": "+saveData.Conquistas[i]+"\n";
        }
        
        return log;
    }

    public static int IDConquista(string nomeConquista){
        int i;
        for(i=0;i<NUM_CONQUISTAS;i++){
            if(nomeConquista == NomesConquistas[i]){
                return i;
            }
        }
        return -1;
    }

    public static void DesbloquearConquista(string nomeConquista){
        int idConquistaAux = IDConquista(nomeConquista);
        SaveData saveData = SaveSystem.CarregarData();
        if(!saveData.Conquistas[idConquistaAux]){
            SalvarConquista(idConquistaAux);
            Debug.Log("Conquista Desbloqueada: "+nomeConquista);
        }
    }

    public static void AdicionarTotem(int _numTotems){
        SaveData saveData = SaveSystem.CarregarData();
        int numTotems = saveData.Totems;
        numTotems += _numTotems;
        saveData.Totems = numTotems;
        SaveSystem.SalvarAlteraçõesSave(saveData,true);
        VerificarConquistaIconoclasta(numTotems);
    }
    public static void AdicionarVitoria(){
        SaveData saveData = SaveSystem.CarregarData();
        saveData.Vitorias++;
        SaveSystem.SalvarData(saveData);
    }

    public static void AdicionarTempoMundoDiego(float _tempoMundoDiego){
        SaveData saveData = SaveSystem.CarregarData();
        saveData.TempoMundoDiego += TempoArredondado(_tempoMundoDiego);
        SaveSystem.SalvarAlteraçõesSave(saveData,false);

        VerificarConquistaEntusiastaOcultismo();
    }

    public static float TempoArredondado(float _tempo){
        return (float) Math.Round(_tempo * 100f) / 100f;
    }

    public static void VerificarConquistaEntusiastaOcultismo(){
        SaveData saveData = SaveSystem.CarregarData();
        if(saveData.TempoMundoDiego >= TEMPO_CONQUISTA_ENTUSIASTA_OCULTISMO){
            DesbloquearConquista("Entusiasta do Ocultismo");
        }
    }

    public static void VerificarConquistaIssoNaoEstaAcontecendo(float _tempo){
        if(_tempo >= (_TEMPO_CONQUISTA_ISSONÃOESTÁACONTECENDO)){
            DesbloquearConquista("Isso Não Está Acontecendo");
        }
    }

    public static void VerificarConquistasVitorias(){
        SaveData saveData = SaveSystem.CarregarData();
        int numVitorias = saveData.Vitorias;
        numVitorias++;
        saveData.Vitorias = numVitorias;
        //SaveSystem.SalvarAlteraçõesSave(saveData);

        switch(numVitorias){
            case 1:{DesbloquearConquista("Sobrevivente I"); return;}
            case 2:{DesbloquearConquista("Sobrevivente II"); return;}
            case 3:{DesbloquearConquista("Sobrevivente III"); return;}
        }

        RegistrarFinalConcluido(saveData,0);
    }

    public static int NumeroVitorias(){
        SaveData saveData = SaveSystem.CarregarData();
        return saveData.Vitorias;
    }

    public static void VerificarConquistaIconoclasta(int _numTotems){
        if(_numTotems >= NUM_TOTEMS_CONQUISTA){
            DesbloquearConquista("Iconoclasta");
        }
    }

    public static bool[] ConquistasConcluidas(){
        SaveData save = SaveSystem.CarregarData();
        return save.Conquistas;
    }

    public static bool VerificarFinalConcluido(int indice) {
        SaveData save = SaveSystem.CarregarData();
        return save.Finais[indice];
    }

    public static void RegistrarFinalConcluido(int indice) {
        SaveData save = SaveSystem.CarregarData();
        if(!save.Finais[indice]) {
            save.Finais[indice] = true;
        }
        SaveSystem.SalvarAlteraçõesSave(save,true);
    }

    public static void RegistrarFinalConcluido(SaveData save, int indice) {
        if(!save.Finais[indice]) {
            save.Finais[indice] = true;
        }
        SaveSystem.SalvarAlteraçõesSave(save,false);
    }

    public static void AtivarVideoPreload() {
        SaveData save = SaveSystem.CarregarData();
        if(!save.VideoPreloadAtivado) {
            save.AtivarVideoPreload = true;
        }
        SaveSystem.SalvarAlteraçõesSave(save,false);
    }

    public static bool GetVideoPreloadAtivado() {
        SaveData save = SaveSystem.CarregarData();
        return save.AtivarVideoPreload;
    }

    public static void DesativarVideoPreload() {
        SaveData save = SaveSystem.CarregarData();
        save.AtivarVideoPreload = false;
        save.VideoPreloadAtivado = true;
        SaveSystem.SalvarAlteraçõesSave(save,false);
    }

    public static void DesbloquearConquistaCartaMadeira() {
        DesbloquearConquista("Carta do Madeira");
        RegistrarFinalConcluido(1);
    }
}
