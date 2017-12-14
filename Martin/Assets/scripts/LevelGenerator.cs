using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    

    private Vector2 _lastGroundPosition = new Vector2((float)-32.6452, (float)-1.369345);
    private Vector2 _lastTrapezePosition;
    private Vector2 _lastTrapezeScale;
    private Quaternion _lastTrapezeRotation = new Quaternion(0, 0, 0 , 0);
    private Quaternion _baseTrapezeRotation = new Quaternion(0, 0, 0, 0);
    private Vector2 _lastTrapPosition;
    private float groundSize = (float)19.20;
    public Martin martin;
    public int nbIteration = 0;
    public Transform _groundPrefab;
    public Transform _trapezPrefab;
    public int lastSpace = 0;
    public int _nbTrapezLeftForSerie = 0;

    // Use this for initialization
    void Start ()
    {
        _lastTrapezePosition = new Vector2(0, Loader.getInstance()._trapezeTopPosition);
        _lastTrapezeScale = new Vector2(0, Loader.getInstance()._trapezeLength);
        //var newTrapez = Instantiate(_trapezPrefab) as Transform;
        //_lastTrapezePosition = newTrapez.position = new Vector3(5, _lastTrapezePosition.y);
        //_lastTrapezeRotation = newTrapez.rotation = new Quaternion(0, 0, 0, 0);


    }

    // Update is called once per frame
    void Update ()
    {
        //valeur arbitraire pour continuer a construire
        if (martin.transform.position.x > _lastGroundPosition.x - 1000) {
            nbIteration++;
            int difficulty = nbIteration / 3;
            int nbTrapez = 0;
            if(difficulty >0 && _nbTrapezLeftForSerie == 0)
            {
                _nbTrapezLeftForSerie = difficulty + Random.Range(-1, 1);
                nbTrapez = _nbTrapezLeftForSerie;

            }

            Random rnd = new Random();
            bool firstTrapeze = true;
            while (_nbTrapezLeftForSerie > 0)
            {
                _nbTrapezLeftForSerie--;
                var newTrapez = Instantiate(_trapezPrefab) as Transform;
                if (firstTrapeze)
                {
                    _lastTrapezePosition = newTrapez.position = new Vector3(_lastGroundPosition.x + groundSize/2 +8, _lastTrapezePosition.y);
                    //_lastTrapezeRotation = newTrapez.rotation = new Quaternion(0,0,0,0);
                    firstTrapeze = false;
                }
                else
                {
                    _lastTrapezePosition = newTrapez.position = new Vector3(_lastTrapezePosition.x + 8, _lastTrapezePosition.y);
                   // _lastTrapezeRotation = newTrapez.rotation = new Quaternion(0, 0, 0 , 0);

                }
            }
            var newGround = Instantiate(_groundPrefab) as Transform;
            lastSpace = Random.Range(-5, 10);
            _lastGroundPosition = newGround.position = new Vector3(_lastGroundPosition.x+ groundSize+ nbTrapez*6+6, _lastGroundPosition.y+ Random.Range(0,0));

        }
    }
}

