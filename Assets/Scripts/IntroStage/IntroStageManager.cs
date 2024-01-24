using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CarregarCenaPause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CarregarCenaPause()
    {
        int countLoaded = SceneManager.sceneCount;
        bool cenaMenuPausaLoaded = false;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
            if (loadedScenes[i].name == "MenuPausa")
            {
                cenaMenuPausaLoaded = true;
                break;
            }
        }

        if(!cenaMenuPausaLoaded)
        {
            SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
        }
    }

    private void DescarregarCenaPausa()
    {
        SceneManager.UnloadSceneAsync("MenuPausa");
    }


}
