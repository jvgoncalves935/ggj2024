using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeMenu : MonoBehaviour{
    public Image blackFade;

    public GameObject cCompNormal;
    public GameObject cCompDestruido;
    
    public bool cComp = true;

    void Start(){
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "MainCamera"){
            StartCoroutine(fadeIn());
        }
    }

    IEnumerator fadeIn(){
        blackFade.canvasRenderer.SetAlpha(0.0f);
        blackFade.CrossFadeAlpha(1, 2, false);

        yield return new WaitForSeconds(3);
        
        if(cComp){
            cCompNormal.SetActive(false);
            cCompDestruido.SetActive(true);
            cComp = false;
        }else{
            cCompNormal.SetActive(true);
            cCompDestruido.SetActive(false);
            cComp = true;
        }

        blackFade.canvasRenderer.SetAlpha(1.0f);
        blackFade.CrossFadeAlpha(0, 2, false);
    }
}
