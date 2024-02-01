using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkippableCutscenes : MonoBehaviour
{
    public bool inputSkip;

    [SerializeField] public static GameObject instanciaSkippableCutscenes;
    private static SkippableCutscenes _instanciaSkippableCutscenes;
    public static SkippableCutscenes InstanciaSkippableCutscenes {
        get {
            if(_instanciaSkippableCutscenes == null) {
                _instanciaSkippableCutscenes = instanciaSkippableCutscenes.GetComponent<SkippableCutscenes>();
            }
            return _instanciaSkippableCutscenes;
        }
    }

    void Awake() {
        instanciaSkippableCutscenes = FindObjectOfType<SkippableCutscenes>().gameObject;
    }
    public IEnumerator WaitForSecondsCancelavel(float tempo) {
        for(float i = 0;i <= tempo;i += Time.deltaTime) {
            if(inputSkip && Time.timeScale == 1) {
                break;
            }
            yield return null;
        }
        //Frame extra para que o skip não pule duas cutscenes de uma vez.
        yield return null;

    }

    private void Update() {
        GetInputsSkip();
    }

    private void GetInputsSkip() {
        if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
            inputSkip = true;
        } else {
            inputSkip = false;
        }
    }

    public void SetInputSkip(bool flag) {
        inputSkip = flag;
    }
}
