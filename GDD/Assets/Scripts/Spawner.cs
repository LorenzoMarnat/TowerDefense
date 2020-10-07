using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Spawner : MonoBehaviour
{
    public GameObject weakEnnemyPrefab;
    public GameObject mediumEnnemyPrefab;
    public GameObject strongEnnemyPrefab;
    public float reloadTime = 2;
    [HideInInspector]
    public int wave;

    private float reloadProgress;
    private bool strongerEnnemy;
    // Start is called before the first frame update
    void Start()
    {
        reloadProgress = 0;
        wave = 1;
        strongerEnnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime && wave <= 5)
        {
            GameObject go = null;
            switch (wave)
            {
                case 1:
                    go = Instantiate(weakEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    break;

                case 2:
                    if (!strongerEnnemy)
                    {
                        go = Instantiate(weakEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                        strongerEnnemy = true;
                    }
                    else
                    {
                        go = Instantiate(mediumEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                        strongerEnnemy = false;
                    }
                    break;

                case 3:
                    go = Instantiate(mediumEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    break;

                case 4:
                    if (!strongerEnnemy)
                    {
                        go = Instantiate(mediumEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                        strongerEnnemy = true;
                    }
                    else
                    {
                        go = Instantiate(strongEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                        strongerEnnemy = false;
                    }
                    break;

                case 5:
                    go = Instantiate(strongEnnemyPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    break;
            }
            go.GetComponent<FollowPath>().pathCreator = GetComponent<PathCreator>();
            reloadProgress = 0;
        }


    }
}
