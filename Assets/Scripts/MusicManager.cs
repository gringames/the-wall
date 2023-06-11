using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicManager : MonoBehaviour
{
    public enum Character { Circle, Square, Triangle}

    public static MusicManager Instance
    {
        get => _instance;
    }

    [SerializeField] private AudioSource _themeTriangle;

    [SerializeField] private AudioSource _themeCirce;

    [SerializeField] private AudioSource _themeSquare;

    [SerializeField] private float _fadeSpeed = 0.5f;
    [SerializeField] private float _maxVolume = 0.65f;

    private List<AudioSource> _sources = new();

    private static MusicManager _instance;

    private float _valueCircle = 1;
    private float _valueSquare = 0;
    private float _valueTriangle = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _sources.Add(_themeCirce);

        _sources.Add(_themeSquare);

        _sources.Add(_themeTriangle);
    }


    public void SetCharacter(Character newCharacter)
    {
        switch (newCharacter)
        {
            case Character.Circle:
                _valueCircle = 1;
                _valueSquare = 0;
                _valueTriangle = 0;
                break;
            case Character.Square:
                _valueCircle = 0;
                _valueSquare = 1;
                _valueTriangle = 0;
                break;
            case Character.Triangle:
                _valueCircle = 0;
                _valueSquare = 0;
                _valueTriangle = 1;
                break;
        }
    }

    private void Update()
    {
        _themeCirce.volume = Mathf.MoveTowards(_themeCirce.volume, _valueCircle * _maxVolume, _fadeSpeed * Time.deltaTime);
        _themeSquare.volume = Mathf.MoveTowards(_themeSquare.volume, _valueSquare * _maxVolume, _fadeSpeed * Time.deltaTime);
        _themeTriangle.volume = Mathf.MoveTowards(_themeTriangle.volume, _valueTriangle * _maxVolume, _fadeSpeed * Time.deltaTime);
    }


}
