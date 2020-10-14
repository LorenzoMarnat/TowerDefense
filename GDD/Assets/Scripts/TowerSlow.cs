using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerSlow : MonoBehaviour
{
    //public GameObject firePrefab;
    public float reloadTime;
    public float range = 3;
    public float slow = 50;
    public float cost = 100;
    [HideInInspector]
    public int upgrades = 0;

    private float reloadProgress = 0;
    private List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        reloadProgress = 0;
        upgrades = 0;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        getTargets();
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime && targets.Count > 0)
        {
            List<GameObject> copy = targets.Where(item => item != null).ToList();
            int size = targets.Count;
            foreach(GameObject target in copy)
            {
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) <= range && !target.GetComponent<FollowPath>().slowed)
                    {
                        target.GetComponent<FollowPath>().speed *= slow/100;
                        target.GetComponent<FollowPath>().slowed = true;
                    }
                    else if (Vector3.Distance(transform.position, target.transform.position) > range && target.GetComponent<FollowPath>().slowed)
                    {
                        target.GetComponent<FollowPath>().speed /= slow/100;
                        target.GetComponent<FollowPath>().slowed = false;
                        targets.Remove(target);
                    }
                }
                else
                    targets.Remove(target);
            }
            reloadProgress = 0;
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

