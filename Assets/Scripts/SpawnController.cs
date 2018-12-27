using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject barreiraPreFab; //objeto a ser renascido
    public float rateSpawn; //intervalo de spawn
    private float currentTime;
    private int posicao;
    private float y;
    public float posA, posB;

	// Use this for initialization
	void Start () {
        currentTime = 0;

    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= rateSpawn)
        {
            currentTime = 0;
            posicao = Random.Range(1,100);
            if (posicao > 50)
            {
                y = posA;
            }
            else
            {
                y = posB;
            }

            //Debug.Log(posicao);
            GameObject tempPrefab = Instantiate(barreiraPreFab) as GameObject;
            tempPrefab.transform.position = new Vector3(transform.position.x, y, tempPrefab.transform.position.z);
        }
	}
}
