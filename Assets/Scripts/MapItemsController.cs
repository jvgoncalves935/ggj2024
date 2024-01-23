using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemsController : MonoBehaviour
{

    [SerializeField] private GameObject coinsObj;
    [SerializeField] private GameObject healthRegensObj;
    private GameObject[] coinsArray;
    private GameObject[] healthRegensArray;
    private int coinsChildrenCount;
    private int healthRegensChildrenCount;

    [SerializeField] public static GameObject instanciaMapItemsController;
    private static MapItemsController _instanciaMapItemsController;
    public static MapItemsController InstanciaMapItemsController {
        get {
            if(_instanciaMapItemsController == null) {
                _instanciaMapItemsController = instanciaMapItemsController.GetComponent<MapItemsController>();
            }
            return _instanciaMapItemsController;
        }
    }

    private void Awake() {
        instanciaMapItemsController = FindObjectOfType<MapItemsController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetCoins();
        GetHealthRegens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetCoins() {
        coinsChildrenCount = coinsObj.transform.childCount;
        coinsArray = new GameObject[coinsChildrenCount];

        for(int i=0; i < coinsChildrenCount;i++) {
            coinsArray[i] = coinsObj.transform.GetChild(i).gameObject;
        }
    }

    public void RestartCoins() {
        for(int i = 0;i < coinsChildrenCount;i++) {
            coinsArray[i].SetActive(true);
        }
    }

    public void RestartItems()
    {
        RestartCoins();
        RestartHealthRegens();
    }


    private void GetHealthRegens()
    {
        healthRegensChildrenCount = healthRegensObj.transform.childCount;
        healthRegensArray = new GameObject[healthRegensChildrenCount];

        for (int i = 0; i < healthRegensChildrenCount; i++)
        {
            healthRegensArray[i] = healthRegensObj.transform.GetChild(i).gameObject;
        }
    }

    public void RestartHealthRegens()
    {
        for (int i = 0; i < healthRegensChildrenCount; i++)
        {
            healthRegensArray[i].SetActive(true);
        }
    }
}
