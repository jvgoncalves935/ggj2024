using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScene : MonoBehaviour
{
    [SerializeField] public static GameObject instanciaResetScene;
    private static ResetScene _instanciaResetScene;
    public static ResetScene InstanciaResetScene {
        get {
            if(_instanciaResetScene == null) {
                _instanciaResetScene = instanciaResetScene.GetComponent<ResetScene>();
            }
            return _instanciaResetScene;
        }
    }

    private void Awake() {
        instanciaResetScene = FindObjectOfType<ResetScene>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        PlayerController.InstanciaPlayerController.RestartCheckpoint();
        PlayerController.InstanciaPlayerController.RestartCoins();
        MapItemsController.InstanciaMapItemsController.RestartItems();
        EnemiesController.InstanciaEnemiesController.RestartEnemies();
        PlayerController.InstanciaPlayerController.RestartPlayer();
    }
}
