using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public float life = 100;
    public float gold = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>().gold += gold;
            Destroy(gameObject);
        }
    }
}
