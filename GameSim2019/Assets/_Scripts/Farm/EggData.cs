using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EggData
{
    public float[] pos = new float[3];
    public float[] rotation = new float[3];

    public char speed;
    public char glide;
    public char power;

    public int days;
    public int seconds;

    public EggData(Egg egg)
    {

        pos[0] = egg.transform.position.x;
        pos[1] = egg.transform.position.y;
        pos[2] = egg.transform.position.z;

        rotation[0] = egg.transform.rotation.eulerAngles.x;
        rotation[1] = egg.transform.rotation.eulerAngles.y;
        rotation[2] = egg.transform.rotation.eulerAngles.z;

        speed = egg.speed;
        glide = egg.glide;
        power = egg.power;

        days = egg.daysTillHatch;
        seconds = egg.secondsTillHatch;
    }

}
