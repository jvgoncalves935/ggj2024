using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData{
    private bool cartaMadeira;
    private bool noticiaJornal;
    private bool[] conquistas;
    private int vitorias;
    private int totems;
    private float tempoMundoDiego;
    private string linguagem;
    private string nomeArquivoSave;
    private bool[] finais;
    private bool ativarVideoPreload;
    private bool videoPreloadAtivado;
    private bool olhoAtivado;

    public bool CartaMadeira{
        get{
            return cartaMadeira;
        }
        set{
            cartaMadeira = value;
        }
    }
    public bool NoticiaJornal{
        get{
            return noticiaJornal;
        }
        set{
            noticiaJornal = value;
        }
    }
    public bool[] Conquistas{
        get{
            return conquistas;
        }
        set{
            conquistas = value;
        }
    }
    public int Vitorias{
        get{
            return vitorias;
        }
        set{
            vitorias = value;
        }
    }
    public int Totems{
        get{
            return totems;
        }
        set{
            totems = value;
        }
    }
    public float TempoMundoDiego{
        get{
            return tempoMundoDiego;
        }
        set{
            tempoMundoDiego = value;
        }
    }
    public string Linguagem{
        get{
            return linguagem;
        }
        set{
            linguagem = value;
        }
    }
    public string NomeArquivoSave {
        get {
            return nomeArquivoSave;
        }
        set {
            nomeArquivoSave = value;
        }
    }
    public bool[] Finais {
        get {
            return finais;
        }
        set {
            finais = value;
        }
    }
    public bool AtivarVideoPreload {
        get {
            return ativarVideoPreload;
        }
        set {
            ativarVideoPreload = value;
        }
    }
    public bool VideoPreloadAtivado {
        get {
            return videoPreloadAtivado;
        }
        set {
            videoPreloadAtivado = value;
        }
    }
    public bool OlhoAtivado {
        get {
            return olhoAtivado;
        }
        set {
            olhoAtivado = value;
        }
    }
    public SaveData(bool _cartaMadeira, bool _noticiaJornal, bool[] _conquistas, int _totems, int _vitorias, float _tempoMundoDiego, string _linguagem, string _nomeArquivoSave, bool[] _finais, bool _ativarVideoPreload, bool _videoPreloadAtivado, bool _olhoAtivado){
        cartaMadeira = _cartaMadeira;
        noticiaJornal = _noticiaJornal;
        conquistas = _conquistas;
        totems = _totems;
        vitorias = _vitorias;
        tempoMundoDiego = _tempoMundoDiego;
        linguagem = _linguagem;
        nomeArquivoSave = _nomeArquivoSave;
        finais = _finais;
        ativarVideoPreload = _ativarVideoPreload;
        videoPreloadAtivado = _videoPreloadAtivado;
        olhoAtivado = _olhoAtivado;
    }
}
