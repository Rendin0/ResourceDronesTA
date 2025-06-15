using System;

[Serializable]
public class SaveData
{
    public float Crystals = 100f;

    public int[] Drones = new int[4]
    {
        0, 
        0, 
        0, 
        0  
    };

    public float Volume = 0.3f;

    public Localisation Localisation = Localisation.en;
}