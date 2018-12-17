using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour {


    public GameObject[] npcs;
    GameObject npcInstance;

    public Transform spawner;
    private float timer = 7.5f;
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        int num = Random.Range(0, npcs.Length + 1);

        switch(num)
        {
            case 0:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[0], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;

            case 1:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[1], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;
            case 2:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[2], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;
            case 3:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[3], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;
            case 4:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[4], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;
            case 5:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[5], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;
            case 6:
                if (timer <= 0f)
                {
                     npcInstance = Instantiate(npcs[6], spawner.position, spawner.rotation) as GameObject;
                    timer = 10f;

                }
                break;


        }






    }
}
