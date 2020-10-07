using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerMulti : MonoBehaviour
{
    public GameObject firePrefab;
    public float reloadTime;
    public float range = 3;
    public float damage = 20;

    private float reloadProgress = 0;
    private List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        reloadProgress = 0;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        getTargets();
        reloadProgress += Time.deltaTime;
        if (targets.Count > 0)
        {
            if (targets.First() == null)
                targets.RemoveAt(0);
            else
            {
                if (reloadProgress >= reloadTime && Vector3.Distance(transform.position, targets.First().transform.position) <= range)
                {
                    GameObject go = Instantiate(firePrefab, transform.position, Quaternion.identity);

                    go.GetComponent<Fire>().target = targets.First();
                    go.GetComponent<Fire>().damage = damage;

                    reloadProgress = 0;
                }
                else if (Vector3.Distance(transform.position, targets.First().transform.position) > range)
                    targets.RemoveAt(0);
            }
        }
    }

    private void getTargets()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject ennemy in ennemies)
        {
            if (Vector3.Distance(transform.position, ennemy.transform.position) <= range && !targets.Contains(ennemy))
                targets.Add(ennemy);
        }
    }
}
