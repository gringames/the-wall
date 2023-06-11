using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private AudioSource _beatTriangle;

    [SerializeField] private AudioSource _themeCirce;
    [SerializeField] private AudioSource _beatCircle;

    [SerializeField] private AudioSource _themeSquare;
    [SerializeField] private AudioSource _beatSquare;

    [SerializeField] private float _fadeSpeed = 0.5f;

    private List<AudioSource> _sources = new();

    private static MusicManager _instance;

    private Character _currentCharacter;

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

        _sources.Add(_beatCircle);
        _sources.Add(_themeCirce);

        _sources.Add(_beatSquare);
        _sources.Add(_themeSquare);

        _sources.Add(_beatTriangle);
        _sources.Add(_themeTriangle);
    }


    public void SetCharacter(Character newCharacter)
    {
        if (newCharacter == _currentCharacter)
        {
            return;
        }

        switch (newCharacter)
        {
            case Character.Circle:
                Fade(_themeCirce, _beatSquare, _beatTriangle);
                break;
            case Character.Square:
                Fade(_themeSquare, _beatCircle, _beatTriangle);
                break;
            case Character.Triangle:
                Fade(_themeTriangle, _beatCircle, _beatSquare);
                break;
        }
    }


    private void Fade(AudioSource theme, AudioSource beat1, AudioSource beat2)
    {
        StartCoroutine(DoFade(theme, beat1, beat2));
    }

    private IEnumerator DoFade(AudioSource theme, AudioSource beat1, AudioSource beat2)
    {
        while (theme.volume != 1)
        {
            foreach (AudioSource source in _sources)
            {
                if (source != theme || source != beat1 || source != beat2)
                {
                    source.volume = Mathf.MoveTowards(source.volume, 0, _fadeSpeed);
                }
            }

            theme.volume = Mathf.MoveTowards(theme.volume, 1, _fadeSpeed);
            beat1.volume = Mathf.MoveTowards(beat1.volume, 1, _fadeSpeed);
            beat2.volume = Mathf.MoveTowards(beat2.volume, 1, _fadeSpeed);

            yield return new WaitForFixedUpdate();
        }
    }
}
