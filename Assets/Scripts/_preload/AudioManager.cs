using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string,int> soundsLibraryIndex;
    [SerializeField] private Sound[] soundsLibrary;
    private int indiceAtual;
    private bool wasPlaying;
    private int[] indexSoundsCurrentScene;

    [SerializeField] private AudioMixerGroup musicasAudioMixerGroup;
    [SerializeField] private AudioMixerGroup sonsAudioMixerGroup;
    [SerializeField] private AudioMixerGroup vozesAudioMixerGroup;

    private List<AudioSource> sonsList;
    private List<SoundClip> soundClipList;

    [SerializeField] private static AudioManager _instanciaAudioManager;
    [SerializeField] public static GameObject instanciaAudioManager;
    [SerializeField] private AudioSource audioSourceMusicas;
    public static AudioManager InstanciaAudioManager {
        get {
            if(_instanciaAudioManager == null) {
                _instanciaAudioManager = instanciaAudioManager.GetComponent<AudioManager>();
            }
            return _instanciaAudioManager;
        }
    }

    private void Awake() {
        instanciaAudioManager = FindObjectOfType<AudioManager>().gameObject;
        DontDestroyOnLoad(instanciaAudioManager);
        audioSourceMusicas = gameObject.AddComponent<AudioSource>();
        IniciarDictGlobal();
        IniciarAudios();
        
        wasPlaying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        IniciarSonsList();
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        IniciarSonsList();
    }
    private void OnSceneUnloaded(Scene scene) {
        LimparSonsList();
    }

    private void IniciarDictGlobal(){
        soundsLibraryIndex = new Dictionary<string, int>();

        int i;
        for(i=0; i<soundsLibrary.Length; i++){
            soundsLibraryIndex.Add(soundsLibrary[i].name, i);
        }
    }

    private void IniciarAudios(){
        foreach(Sound s in soundsLibrary){
            //s.soundClip = gameObject.AddComponent<SoundClip>();
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.awake;

            if(s.musica){
                RegistrarMusica(s.source);
            }

            if(s.som){
                RegistrarSom(s.source);
            }

            VerificarAudioMixer(s.source);
        }

        indexSoundsCurrentScene = new int[soundsLibrary.Length];
        for(int i = 0;i < indexSoundsCurrentScene.Length;i++){
            indexSoundsCurrentScene[i] = i;
        }
    }

    private void DescarregarAudios(){
        //Os Audio Clips serão todos descarregados. Futuramente serão gradativamente carregados apenas os clips das cenas atuais.
        int i;
        int tam = indexSoundsCurrentScene.Length;
        for(i = 0;i < tam;i++){
            //Debug.Log("descarregou " + soundsLibrary[indexSoundsCurrentScene[i]].name);
            soundsLibrary[indexSoundsCurrentScene[i]].clip.UnloadAudioData();
        }
    }

    public int Find(string nome){
        int indice = -1;
        if(!soundsLibraryIndex.TryGetValue(nome, out indice)) {
            Debug.LogWarning("Audio \"" + nome + "\" não encontrado.");
        }
        return indice;
    }

    public void Play(string nome){
        int indice = Find(nome);
        if(indice == -1){
            return;
        }
        //Debug.Log("play "+nome);

        soundsLibrary[indice].source.Play();
        indiceAtual = indice;
    }

    public void Play(int indice){
        soundsLibrary[indice].source.Play();
        indiceAtual = indice;

        //Debug.Log("play " + soundsLibrary[indice].name);
    }

    public void Stop(string nome) {
        int indice = Find(nome);
        if(indice == -1){
            return;
        }

        soundsLibrary[indice].source.Stop();
    }

    public void Stop(int indice){
        soundsLibrary[indice].source.Stop();
    }

    public void StopMusicaAtual() {
        soundsLibrary[indiceAtual].source.Stop();
    }

    private void RegistrarMusica(AudioSource source){
        source.outputAudioMixerGroup = musicasAudioMixerGroup;
    }

    private void RegistrarSom(AudioSource source) {
        source.outputAudioMixerGroup = sonsAudioMixerGroup;
    }

    private void VerificarAudioMixer(AudioSource source){
        if(source.outputAudioMixerGroup == null){
            Debug.LogWarning("Audio \"" + source.name + "\" não foi atribuido a nenhum AudioMixer.");
        }
    }

    public void IniciarSonsCenaAtual(){
        //Descarregar os audios da cena anterior.
        DescarregarAudios();

        //Carregar apenas os audios da cena atual.
        SoundClip[] sonsCena = FindObjectsOfType<SoundClip>();
        indexSoundsCurrentScene = new int[sonsCena.Length];

        int i = 0;
        foreach(SoundClip s in sonsCena){
            //Debug.Log("carregou "+s.nome);
            if(!s.efeitoSonoro) {
                int indice = Find(s.nome);
                soundsLibrary[indice].clip.LoadAudioData();
                indexSoundsCurrentScene[i] = indice;
                i++;
            }
        }
    }

    //As musicas tocam uma de cada vez, então é possível usar apenas uma variavel para saber se a música estava tocando.
    public void PausarMusicaMenuPausa() {
        wasPlaying = false;
        if(soundsLibrary[indiceAtual].source.isPlaying){
            wasPlaying = true;
            soundsLibrary[indiceAtual].source.Pause();
        }
    }

    public void DespausarMusicaMenuPausa() {
        if(wasPlaying){
            soundsLibrary[indiceAtual].source.UnPause();
        }
    }

    private List<SoundClip> GetAllObjectsOnlyInScene() {
        List<SoundClip> objectsInScene = new List<SoundClip>();

        foreach(SoundClip go in Resources.FindObjectsOfTypeAll(typeof(SoundClip)) as SoundClip[]) {
            //if(!UnityEditor.EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                if(go.efeitoSonoro) {
                    objectsInScene.Add(go);
                }
                
        }

        return objectsInScene;
    }

    public void IniciarSonsList() {
        sonsList = new List<AudioSource>();
        soundClipList = new List<SoundClip>();
        List<SoundClip> objetosCena = GetAllObjectsOnlyInScene();

        foreach(SoundClip obj in objetosCena) {
            AdicionarSomList(obj.GetComponent<AudioSource>());
            AdicionarSoundClipList(obj);
            //Debug.Log("aaaaaaa " + obj.name);   
        }
    }

    public void AdicionarSomList(AudioSource audioSource) {
        sonsList.Add(audioSource);
    }

    public void AdicionarSoundClipList(SoundClip soundClip) {
        soundClipList.Add(soundClip);
    }

    public void PausarSons() {
        int i = 0;
        foreach(SoundClip soundclip in soundClipList) {
            if(soundclip.gameObject.activeInHierarchy && soundclip.isPlaying()) {
                soundclip.SetWasPlaying(true);
                soundclip.Pause();
            }
            i++;
        }
    }

    public void PlaySons() {
        int i = 0;
        foreach(SoundClip soundClip in soundClipList) {
            if(soundClip.gameObject.activeInHierarchy && soundClip.WasPlaying() && !soundClip.IsIgnorarPause()){
                soundClip.UnPause();
            }
            i++;
        }
    }

    public void LimparSonsList() {
        sonsList = new List<AudioSource>();
        soundClipList = new List<SoundClip>();
    }

}
