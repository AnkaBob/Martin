using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    private Vector2 _lastGroundPosition = new Vector2((float)-32.6452, (float)-1.369345);
    private Vector2 _lastTrapezePosition;
    private Vector2 _lastTrapPosition;
    private float groundSize = (float)19.20;
    public Martin martin;
    public int nbIteration = 0;
    public Transform _groundPrefab;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //valeur arbitraire pour continuer a construire
        if (martin.transform.position.x > _lastGroundPosition.x - 1000) {
            nbIteration++;
            Random rnd = new Random();
            var newGround = Instantiate(_groundPrefab) as Transform;
            _lastGroundPosition = newGround.position = new Vector3(_lastGroundPosition.x+ groundSize+ Random.Range(-5, 5), _lastGroundPosition.y+ Random.Range(-2,2));
        }
    }
}

