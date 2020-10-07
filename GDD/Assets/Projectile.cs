using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    public float speed = 10;
    [HideInInspector]
    public float damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);

            if (transform.position == target.transform.position)
            {
                target.GetComponent<Alive>().life -= damage;
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }
}
