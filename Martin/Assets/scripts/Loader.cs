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



        print ("fin");

		//BuyableEventDataManager.EVENTS.Add (data._id, data);

	}
}
	
