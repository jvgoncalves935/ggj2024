using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkippableCutscenes : MonoBehaviour
{
    public bool mouseClick;

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
            if(mouseClick && Time.timeScale == 1) {
                yield break;
            }
            yield return null;
        }
    }

    private void Update() {
        GetMouseClick();
    }

    private void GetMouseClick() {
        if(Input.GetMouseButtonDown(0)) {
            mouseClick = true;
        } else {
            mouseClick = false;
        }
    }

    public void SetMouseClick(bool flag) {
        mouseClick = flag;
    }
}
