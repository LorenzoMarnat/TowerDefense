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
    private GameObject towerToUpgrade = null;
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

    public void upgrade()
    {
        if(towerToUpgrade != null)
            upgradeTower(towerToUpgrade);
    }
    private void upgradeTower(GameObject go)
    {
        if (go.tag == "Mono" && gold >= 50)
        {
            TowerMono tower = go.GetComponent<TowerMono>();
            if (tower.upgrades < 2)
            {
                gold -= 50;
                tower.damage += 10;
                tower.reloadTime -= 0.1f;
                tower.upgrades += 1;
            }
        }

        if (go.tag == "Multi" && gold >= 50)
        {
            TowerMulti tower = go.GetComponent<TowerMulti>();
            if (tower.upgrades < 2)
            {
                gold -= 50;
                tower.damage += 10;
                tower.firePrefab.GetComponent<Fire>().range += 1;
                tower.upgrades += 1;
            }
        }

        if (go.tag == "Slow" && gold >= 50)
        {
            TowerSlow tower = go.GetComponent<TowerSlow>();
            if (tower.upgrades < 2)
            {
                gold -= 50;
                tower.slow += 10;
                tower.upgrades += 1;
            }
        }
    }

    private void Clicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Floor")
            {
                Vector3 position = go.transform.position + new Vector3(0,0.7f,0);
                if (towerToInstantiate == 0 && gold >= towerMono.GetComponent<TowerMono>().cost)
                {
                    gold -= towerMono.GetComponent<TowerMono>().cost;
                    Instantiate(towerMono, position, Quaternion.identity);
                    go.GetComponent<BoxCollider>().enabled = false;
                }

                if (towerToInstantiate == 1 && gold >= towerMulti.GetComponent<TowerMulti>().cost)
                {
                    gold -= towerMulti.GetComponent<TowerMulti>().cost;
                    Instantiate(towerMulti, position, Quaternion.identity);
                    go.GetComponent<BoxCollider>().enabled = false;
                }

                if (towerToInstantiate == 2 && gold >= towerSlow.GetComponent<TowerSlow>().cost)
                {
                    gold -= towerSlow.GetComponent<TowerSlow>().cost;
                    Instantiate(towerSlow, position, Quaternion.identity);
                    go.GetComponent<BoxCollider>().enabled = false;
                }
            }

            if (go.tag == "Mono" || go.tag == "Multi" || go.tag == "Slow")
            {
                if (go != towerToUpgrade)
                {
                    if(towerToUpgrade != null)
                    {
                        Outline ol = towerToUpgrade.GetComponent<Outline>();
                        Destroy(ol);
                    }
                        
                    Outline outline = go.gameObject.AddComponent<Outline>();

                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.yellow;
                    outline.OutlineWidth = 5f;

                    towerToUpgrade = go;
                }
            }
        }
    }
}
