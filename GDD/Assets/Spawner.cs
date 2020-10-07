using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Spawner : MonoBehaviour
{
	public GameObject ennemyPrefab;
	public float reloadTime = 5;
	public float reloadProgress = 0;

	// Start is called before the first frame update
	void Start()
	{
		reloadProgress = 0;

	}

	// Update is called once per frame
	void Update()
	{
		reloadProgress += Time.deltaTime;
			if (reloadProgress >= reloadTime)
			{
				GameObject go = Instantiate(ennemyPrefab, new Vector3(10, 10, 10), Quaternion.identity);
			go.GetComponent<FollowPath>().pathCreator = GetComponent<PathCreator>();
				reloadProgress = 0;
			}
	}
}
