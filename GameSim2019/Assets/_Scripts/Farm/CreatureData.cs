using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class represents the AIStatSheet
[System.Serializable]
public class CreatureData 
{

    public float[] pos = new float[3];
    public float[] rotation = new float[3];

    public string name;
    public string bodyType;

    public float maxStamina;
    public char staminaGrade;

    public float power;
    public float maxPower;
    public char powerGrade;

    public float speed;
    public float maxSpeed;
    public char speedGrade;

    public float glide;
    public float maxGlide;
    public char glideGrade;

    public float maxHunger;
    public float currentHunger;
    public float hungerLowerRate;

    public bool isStarving;

    public float happiness;

    public bool wantsBaby;

    public int age;

    public int ID;

    public int daysTillNextBaby;

    public CreatureData ( AIStatSheet stats )
    {

        pos[0] = stats.transform.position.x;
        pos[1] = stats.transform.position.y;
        pos[2] = stats.transform.position.z;

        rotation[0] = stats.transform.rotation.eulerAngles.x;
        rotation[1] = stats.transform.rotation.eulerAngles.y;
        rotation[2] = stats.transform.rotation.eulerAngles.z;

        this.name = stats.myName;
        this.bodyType = stats.GetBodyName();

        this.maxStamina = stats.MaxStamina();
        this.staminaGrade = stats.StaminaGrade();

        this.power = stats.power;
        this.maxPower = stats.MaxPower();
        this.powerGrade = stats.PowerGrade();

        this.speed = stats.speed;
        this.maxSpeed = stats.MaxSpeed();
        this.speedGrade = stats.SpeedGrade();

        this.glide = stats.glide;
        this.maxGlide = stats.MaxGlide();
        this.glideGrade = stats.GlideGrade();


        this.maxHunger = stats.maxHunger;
        this.currentHunger = stats.currentHunger;
        this.hungerLowerRate = stats.hungerLowerRate;

        this.isStarving = stats.IsStarving();
        this.happiness = stats.happiness;

        this.wantsBaby = stats.gameObject.GetComponent<AIFarmBehavior>().wantsBaby;

        this.age = stats.age;
        this.ID = stats.ID;

        this.daysTillNextBaby = stats.daysTillNextBaby;
    }

}
