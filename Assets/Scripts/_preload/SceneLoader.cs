using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string proximaCena{ get; set; }
    [SerializeField] private static SceneLoader _instanciaSceneLoader;
    [SerializeField] public static GameObject instanciaSceneLoader;

    private string[] nomesCenas;
    public static SceneLoader InstanciaSceneLoader {
        get {
            if(_instanciaSceneLoader == null) {
                _instanciaSceneLoader = instanciaSceneLoader.GetComponent<SceneLoader>();
            }
            return _instanciaSceneLoader;
        }
    }

    private void Awake() {
        instanciaSceneLoader = FindObjectOfType<SceneLoader>().gameObject;
        DontDestroyOnLoad(instanciaSceneLoader);
        NomesCenas();
    }

    public string GetProximaCena(){
        return proximaCena;
    }

    public void SetProximaCena(string cena){
        proximaCena = cena;
    }

    private void NomesCenas(){
        int numeroCenas = SceneManager.sceneCountInBuildSettings;
        nomesCenas = new string[numeroCenas];
        for(int i=0; i < numeroCenas; i++){
            string aux = SceneUtility.GetScenePathByBuildIndex(i);
            int slash = aux.LastIndexOf('/');
            string name = aux.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            nomesCenas[i] = name.Substring(0, dot);
        }
    }

    public int GetIndiceProximaCena(string cena){
        for(int i = 0;i < nomesCenas.Length;i++){
            if(nomesCenas[i] == cena){
                return i;
            }
        }
        return -1;
    }
}
