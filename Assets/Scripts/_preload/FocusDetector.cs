using UnityEngine;

public class FocusDetector : MonoBehaviour
{
    [SerializeField] private static FocusDetector _instanciaFocusDetector;
    [SerializeField] public static GameObject instanciaFocusDetector;
    private float timeScaleAtual;
    public static FocusDetector InstanciaFocusDetector {
        get {
            if(_instanciaFocusDetector == null) {
                _instanciaFocusDetector = instanciaFocusDetector.GetComponent<FocusDetector>();
            }
            return _instanciaFocusDetector;
        }
    }

    private void Awake() {
        instanciaFocusDetector = FindObjectOfType<FocusDetector>().gameObject;
        DontDestroyOnLoad(instanciaFocusDetector);
    }

    private void OnApplicationFocus(bool focus) {
        if(focus){
            ContinuarJogo();
        } else{
            PararJogo();
        }
    }

    private void PararJogo(){
        timeScaleAtual = Time.timeScale;
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }

    private void ContinuarJogo(){
        Time.timeScale = timeScaleAtual;
        AudioListener.pause = false;
    }
}
