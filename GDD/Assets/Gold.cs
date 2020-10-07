using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Gold : MonoBehaviour
{
    public GameObject goldText;
    public GameObject towerText;

    public GameObject towerMono;
    public GameObject towerMulti;
    public GameObject towerSlow;

    [HideInInspector]
    public float gold;
    public float startGold = 100;

    private int towerToInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        gold = startGold;
        towerToInstantiate = -1;
        towerText.GetComponent<Text>().text = "Select a tower";
    }

    // Update is called once per frame
    void Update()
    {
        goldText.GetComponent<Text>().text = "Gold: " + gold.ToString();

        if (Input.GetKeyDown(KeyCode.A))
            monoSelected();

        if (Input.GetKeyDown(KeyCode.Z))
            multiSelected();

        if (Input.GetKeyDown(KeyCode.E))
            slowSelected();

        if (Input.GetMouseButtonDown(0))
            Clicked();
    }
    public void monoSelected()
    {
        towerText.GetComponent<Text>().text = "Selected: Tower Mono";
        towerToInstantiate = 0;
    }
    public void multiSelected()
    {
        towerText.GetComponent<Text>().text = "Selected: Tower Multi";
        towerToInstantiate = 1;
    }
    public void slowSelected()
    {
        towerText.GetComponent<Text>().text = "Selected: Tower Slow";
        towerToInstantiate = 2;
    }
    private void Clicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (towerToInstantiate == 0 && gold >= towerMono.GetComponent<TowerMono>().cost)
            {
                gold -= towerMono.GetComponent<TowerMono>().cost;
                Instantiate(towerMono, hit.point, Quaternion.identity);
            }

            if (towerToInstantiate == 1 && gold >= towerMulti.GetComponent<TowerMulti>().cost)
            {
                gold -= towerMulti.GetComponent<TowerMulti>().cost;
                Instantiate(towerMulti, hit.point, Quaternion.identity);
            }

            if (towerToInstantiate == 2 && gold >= towerSlow.GetComponent<TowerSlow>().cost)
            {
                gold -= towerSlow.GetComponent<TowerSlow>().cost;
                Instantiate(towerSlow, hit.point, Quaternion.identity);
            }
        }
    }
}
