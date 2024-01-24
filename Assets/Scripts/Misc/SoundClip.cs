using UnityEngine;

public class SoundClip : MonoBehaviour
{
    private int indice;
    public string nome;
    private AudioSource audioSource;
    [SerializeField] private bool playOnStart;
    [SerializeField] public bool efeitoSonoro;
    [SerializeField] public bool wasPlaying;
    [SerializeField] public bool ignorarPause;

    private void Awake() {
        GetAudioSource();
    }

    private void Start(){
        if(!efeitoSonoro) {
            indice = AudioManager.InstanciaAudioManager.Find(nome);
        }

        //Debug.Log("nome: "+nome+", indice: "+indice);
        if(playOnStart){
            Play();
        }
    }

    private void GetAudioSource() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(){
        audioSource.Play();
    }

    public void Stop(){
        audioSource.Stop();
    }
    public void Pause() {
        audioSource.Pause();
    }
    public void UnPause() {
        audioSource.UnPause();
    }

    public void SetWasPlaying(bool flag) {
        wasPlaying = flag;
    }

    public bool WasPlaying() {
        return wasPlaying;
    }

    public bool isPlaying() {
        return audioSource.isPlaying;
    }

    public void SetIgnorarPause(bool flag) {
        ignorarPause = flag;
    }

    public bool IsIgnorarPause() {
        return ignorarPause;
    }

}
