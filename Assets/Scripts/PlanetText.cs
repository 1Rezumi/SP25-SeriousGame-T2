﻿using UnityEngine;
using TMPro;

public class PlanetText : MonoBehaviour
{
    public TextMeshProUGUI textBox1;
    public TextMeshProUGUI textBox2;
    public TextMeshProUGUI textBox3;

    public void OnButtonOnePressed()
    {
        textBox1.text = "Speet\n Skelp";
        textBox2.text = "Normal";
        textBox3.text = "Unbearably hot planet, many wars have been fought over the few water sources that remain.";
    }

    public void OnButtonTwoPressed()
    {
        textBox1.text = "Speet\n Space Corn";
        textBox2.text = "Normal";
        textBox3.text = "This planet's surface is 98% ocean with only a few small uninhabited islands making up its landmasses.";
    }

    public void OnButtonThreePressed()
    {
        textBox1.text = "Skelp\n Space Corn";
        textBox2.text = "Normal";
        textBox3.text = "Jungle planet lush with vegitation... and danger.";
    }
}