using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolIdList : MonoBehaviour
{

    private int[] ids;
    // Use this for initialization
    void Awake ()
    {
        BuildIdList();
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public int[] PoolIds ()
    {
        return ids;
    }

    private void BuildIdList ()
    {
        int num = 0;

        foreach ( Transform child in transform )
        {
            ids[num++] = GetComponentInChildren<Item>().GetId();
        }

    }
}
