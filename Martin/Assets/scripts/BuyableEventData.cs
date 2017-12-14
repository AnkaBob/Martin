using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyableEventData {
    public string _eventTitle;

    public int _id;

    public float _costCoeff;
    public int _cost;


    //peut être négatif
    public float _incomeCoeff;
    public int _incomeDuration;
    public int _income;

    public float _karmaCost;

    //durée de vie de l'évenement en jours (est disponible pendant X tours)
    //Pas fixe, ca servira de compteur à priori
    public int _lifeTime;


    //Positif si on doit l'appliquer quand applicable (négatif sinon)
	private int _score;

	//Event et nombre de tours avant de l'appliquer par id
	public Dictionary<int, List<int>> _eventsToAddToTurn = new Dictionary<int, List<int>>();

    //Event et nombre de tours avant de l'appliquer
    public Dictionary<BuyableEventData, int> _eventsToAdd;
    public Dictionary<BuyableEventData, int> _eventsToRemove;

}
