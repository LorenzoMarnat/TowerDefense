using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    public GameObject waveText;
    public float timeBetweenWaves = 10;

    private float waveProgress;
    private int wave = 1;
    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        waveProgress = 0;
        waveText.GetComponent<Text>().text = "Wave: 1";
    }

    // Update is called once per frame
    void Update()
    {
        waveProgress += Time.deltaTime;
        if (waveProgress >= timeBetweenWaves && wave <= 5)
        {
            wave++;
            waveProgress = 0;
            Spawner[] spawners = FindObjectsOfType<Spawner>();

            foreach(Spawner spawner in spawners)
            {
                spawner.reloadTime -= 0.5f;
                spawner.wave++;
            }
            if(wave <= 5)
                waveText.GetComponent<Text>().text = "Wave: " + wave.ToString();
        }
    }
}
