using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;
public class Fire : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    public float speed = 10;
    [HideInInspector]
    public float damage = 20;
    public float range = 3;
    private List<GameObject> targets;
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (transform.position == target.transform.position)
            {
                getTargets();
                List<GameObject> copy = targets.Where(item => item != null).ToList();
                foreach (GameObject ennemy in copy)
                    if(ennemy != null)
                        ennemy.GetComponent<Alive>().life -= damage;
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
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
