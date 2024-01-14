using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "ResearchProject",menuName = "Moon Games/CN Space pirates/Create Research Project")]
public class SOResearchProject : ScriptableObject
{
    //Todo: Verileri obscured(hafıza tarayıcılara karşı korumalı) yapmak
    public string Key;
    public string Title;
    public string Description;
    public Sprite Avatar;

    // Bu projeyi araştırabilmek için tamamlanmış olması gereken araştırma projeleri
    public string[] Dependencies;
    public bool Completed = false;

    public int Resource1;
    public int Resource2;
    public int Resource3;
    public int Resource4;
    public int MinLevel = 1;
    
    //Lazım olan özellikler 
    //Araştırmanın ne kadar süreceği 
    public int TotalDuration;
    //Araştırmanın güncel koşullarda ne kadar süreceği
    public int Duration;
    public DateTime TimeStarted = DateTime.MinValue; // == Min Value ? başlamamış demektir.


}
