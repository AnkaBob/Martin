using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class Loader : MonoBehaviour {

    private static Loader _instance;
    public float _martinSpeed = 10;
    public float _jumpPuissanceMax = 10;
    public float _jumpMinTime = 200;
    public float _jumpMaxTime = 400;
    public float _trapezeSpeed = 10;
    public float _trapezeTopPosition = 10;
    public float _trapezeLength = 10;
    public int _difficultyIncreaseSpeed=3;
    public int _minAddTrapezeRandom=-1;
    public int _maxAddTrapezeRandom=1;
    public int _gapSize=8;
    public int _minAddGroundHeightRandom=0;
    public int _maxAddGroundHeightRandom=0;


    public static Loader getInstance()
    {
        if (_instance == null)
            _instance = new Loader();
        return _instance;
    }

    public Loader()
    {
        Load();
    }
	public void Load(){
		print ("load");
		List<string> fileLines;
		string filePath = System.IO.Path.Combine (Application.dataPath, "GDDatas/settings.txt");
		var sr = File.OpenText(filePath);
		fileLines = sr.ReadToEnd ().Split ('\n').ToList ();
		foreach (string item in fileLines) {
			ExtractData (item);
		}
		sr.Close();
	}

	public void ExtractData(string rawData){
		print (rawData);
		string[] splittedLine = rawData.Split ('=');

        if (splittedLine[0].Equals("MartinSpeed"))
            _martinSpeed = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("JumpPuissanceMax"))
            _jumpPuissanceMax = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("JumpMinTime"))
            _jumpMinTime = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("JumpMaxTime"))
            _jumpMaxTime = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("TrapezeSpeed"))
            _trapezeSpeed = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("TrapezeTopPosition"))
            _trapezeTopPosition = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("TrapezeLength"))
            _trapezeLength = float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        if (splittedLine[0].Equals("DifficultyIncreaseSpeed"))
            _difficultyIncreaseSpeed = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("MinAddTrapezeRandom"))
            _minAddTrapezeRandom = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("MaxAddTrapezeRandom"))
            _maxAddTrapezeRandom = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("GapSize"))
            _gapSize = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("MinAddGroundHeightRandom"))
            _minAddGroundHeightRandom = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        if (splittedLine[0].Equals("MaxAddGroundHeightRandom"))
            _maxAddGroundHeightRandom = int.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);



        print("fin");

		//BuyableEventDataManager.EVENTS.Add (data._id, data);

	}
}
	
