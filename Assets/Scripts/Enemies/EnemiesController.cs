using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private GameObject enemiesObj;
    private Enemy[] enemiesArray;
    private int countEnemiesPatrol;
    private int enemiesIndex = 0;

    [SerializeField] public static GameObject instanciaEnemiesController;
    private static EnemiesController _instanciaEnemiesController;
    public static EnemiesController InstanciaEnemiesController {
        get {
            if(_instanciaEnemiesController == null) {
                _instanciaEnemiesController = instanciaEnemiesController.GetComponent<EnemiesController>();
            }
            return _instanciaEnemiesController;
        }
    }

    private void Awake() {
        instanciaEnemiesController = FindObjectOfType<EnemiesController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetEnemies() {
        countEnemiesPatrol = enemiesObj.transform.childCount;
        enemiesArray = new Enemy[countEnemiesPatrol];

        for(int i = 0;i < countEnemiesPatrol;i++) {
            enemiesArray[enemiesIndex] = enemiesObj.transform.GetChild(i).gameObject.GetComponent<Enemy>();
            enemiesIndex++;
        }
    }

    public void RestartEnemies() {
        for(int i = 0;i < countEnemiesPatrol;i++) {
            enemiesArray[i].gameObject.SetActive(true);
            enemiesArray[i].Restart();
        }
    }
}
