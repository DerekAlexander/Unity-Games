using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class represents the FarmManager and will save all data(including pets, and potentially other things) in the farm
[System.Serializable]
public class FarmData 
{

    // Pets in the farm
    public CreatureData[] pets;

    public EggData[] eggs;

    public DayNightData dayNight;

    public TutorialData tutorialData;

    public SantaData santaData;

    public HouseData houseItems;

    public CleanUpData clean;

    public TreasureData treasure;

    public int blobieID;

    public FarmData ( FarmManager farm )
    {
        dayNight = new DayNightData(farm.cycle);

        pets = new CreatureData[farm.blobies.Count];

        for ( int i = 0; i < pets.Length; i++ )
        {
            pets[i] = new CreatureData(farm.blobies[i]);
        }

        eggs = new EggData[farm.eggs.Count];

        for ( int i = 0; i < eggs.Length; i++ )
        {
            eggs[i] = new EggData(farm.eggs[i]);
        }

        tutorialData = new TutorialData(farm.tutorialManager);

        santaData = new SantaData(farm.santa);

        houseItems = new HouseData(farm.houseItems);

        clean = new CleanUpData(farm.clean);

        treasure = new TreasureData(farm.treasure);

        blobieID = farm.getBlobieID();

    }

}
