using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{



    private Vector2 _lastGroundPosition = new Vector2((float)-32.6452, (float)-1.369345);
    private Vector2 _lastTrapezePosition;
    private Vector2 _lastTrapezeScale;
    private Quaternion _lastTrapezeRotation = new Quaternion(0, 0, 0, 0);
    private Quaternion _baseTrapezeRotation = new Quaternion(0, 0, 0, 0);
    private Vector2 _lastTrapPosition;
    private float groundSize = (float)19.20;
    public Martin martin;
    public int nbIteration = 0;
    public Transform _groundPrefab;
    public Transform _trapezPrefab;
    public int lastSpace = 0;
    public int _nbTrapezeLeftForSerie = 0;

    public int _difficultyIncreaseSpeed;

    public int _minAddTrapezeRandom;
    public int _maxAddTrapezeRandom;

    public int _gapSize;
    public int _minAddGapSizeRandom;
    public int _maxAddGapSizeRandom;

    public int _minAddGroundHeightRandom;
    public int _maxAddGroundHeightRandom;

    // Use this for initialization
    void Start()
    {
        _lastTrapezePosition = new Vector2(0, Loader.getInstance()._trapezeTopPosition);
        _lastTrapezeScale = new Vector2(1, 0.7f);

        _difficultyIncreaseSpeed = Loader.getInstance()._difficultyIncreaseSpeed;
        _minAddTrapezeRandom = Loader.getInstance()._minAddTrapezeRandom;
        _maxAddTrapezeRandom = Loader.getInstance()._maxAddTrapezeRandom;
        _gapSize = Loader.getInstance()._gapSize;
        _minAddGroundHeightRandom = Loader.getInstance()._minAddGroundHeightRandom;
        _maxAddGroundHeightRandom = Loader.getInstance()._maxAddGroundHeightRandom;
        _minAddGapSizeRandom = Loader.getInstance()._minAddGapSizeRandom;
        _maxAddGapSizeRandom = Loader.getInstance()._maxAddGapSizeRandom;
        //var newTrapez = Instantiate(_trapezPrefab) as Transform;
        //_lastTrapezePosition = newTrapez.position = new Vector3(5, _lastTrapezePosition.y);
        //_lastTrapezeRotation = newTrapez.rotation = new Quaternion(0, 0, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {
        //valeur arbitraire pour continuer a construire
        if (martin.transform.position.x > _lastGroundPosition.x - 1000)
        {
            nbIteration++;
            int difficulty = nbIteration / _difficultyIncreaseSpeed;
            int nbTrapeze = 0;
            if (difficulty > 0 && _nbTrapezeLeftForSerie == 0)
            {
                _nbTrapezeLeftForSerie = difficulty + Random.Range(_minAddTrapezeRandom, _maxAddTrapezeRandom);
                nbTrapeze = _nbTrapezeLeftForSerie;

            }
            bool firstTrapeze = true;
            float nextValueForScale = Random.Range(0.7f, 1.3f);
            float totalDistance = 0;
            if (_nbTrapezeLeftForSerie < 1)
                totalDistance = 3;
            while (_nbTrapezeLeftForSerie > 0)
            {
                _nbTrapezeLeftForSerie--;
                var newTrapez = Instantiate(_trapezPrefab) as Transform;
                if (firstTrapeze)
                {
                    _lastTrapezePosition = newTrapez.position = new Vector3(_lastGroundPosition.x + groundSize / 2 + 8, 8.5f - (1 - nextValueForScale) * 2);
                    totalDistance += groundSize / 2 + 8;
                    firstTrapeze = false;
                }
                else
                {
                    float randomValue = Random.Range(1f, 1.4f);
                    _lastTrapezePosition = newTrapez.position = new Vector3(_lastTrapezePosition.x + _gapSize * 1.5f * randomValue + (1 - nextValueForScale) * 2, 8.5f - (1 - nextValueForScale) * 2);
                    totalDistance += _gapSize * 1.5f * randomValue + (1 - nextValueForScale) * 2;
                }
                _lastTrapezeScale = newTrapez.transform.localScale = new Vector3(nextValueForScale, _lastTrapezeScale.y, 0);
                nextValueForScale = 2 - nextValueForScale;

            }
            var newGround = Instantiate(_groundPrefab) as Transform;
            // lastSpace = Random.Range(-5, 10);
            _lastGroundPosition = newGround.position = new Vector3(_lastGroundPosition.x + groundSize * Random.Range(1f, 1.3f) + totalDistance + Random.Range(_minAddGapSizeRandom, _maxAddGapSizeRandom), _lastGroundPosition.y + Random.Range(_minAddGroundHeightRandom, _maxAddGroundHeightRandom));
        }
    }
}
