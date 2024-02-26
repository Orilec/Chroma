using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NarratorTextDisplay : MonoBehaviour
{
    private class NarratorText
    {
        public readonly TMP_Text NarratorTMPText;
        public readonly string NarratorString;
        public NarratorText(TMP_Text tmpText, string narratorString)
        {
            this.NarratorTMPText = tmpText;
            this.NarratorString = narratorString;
        }
    }
    
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _travelSpeed = 8f;
    [SerializeField] private float _fadeOutDuration = 2f;
    private Coroutine _fade;
    private List<NarratorText> _narratorTexts;
    private bool _triggered;
    private PlayerController _player;

    private void Awake()
    {
        _narratorTexts = new List<NarratorText>();
        _player = FindObjectOfType<PlayerController>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var text = transform.GetChild(i).GetComponent<TMP_Text>();
            if (text != null) _narratorTexts.Add(new NarratorText(text, text.text));

        }

        ResetTexts();
    }

    public void EndDisplay()
    {
        foreach (var narrator in _narratorTexts)
        {
            StartCoroutine(FadeOutText(narrator.NarratorTMPText));
        }
    }

    private void ResetTexts()
    {
        foreach (var narratorText in _narratorTexts)
        {
            narratorText.NarratorTMPText.text = "";
        }
    }

    private IEnumerator FadeOutText(TMP_Text textDisplay)
    {
        float duration = _fadeOutDuration; //Fade out over 2 seconds.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Destroy(textDisplay.gameObject);
    }



    // Lookup table for hex characters.
    static readonly char[] NIBBLE_TO_HEX = new char[]
    {
        '0', '1', '2', '3',
        '4', '5', '6', '7',
        '8', '9', 'A', 'B',
        'C', 'D', 'E', 'F'
    };

    private void FadeTo()
    {
        StopFade();
        _fade = StartCoroutine(FadeText());
    }

    private void StopFade()
    {
        if (_fade != null)
            StopCoroutine(_fade);
    }
    
    private IEnumerator FadeText()
    {
        foreach (var narratorText in _narratorTexts)
        {
            var text = narratorText.NarratorString;
            int length = text.Length;

            // Build a character buffer of our desired text,
            // with a rich text "color" tag around every character.
            var builder = new System.Text.StringBuilder(length * 26);
            Color32 color = narratorText.NarratorTMPText.color;
            for (int i = 0; i < length; i++)
            {
                builder.Append("<color=#");
                builder.Append(NIBBLE_TO_HEX[color.r >> 4]);
                builder.Append(NIBBLE_TO_HEX[color.r & 0xF]);
                builder.Append(NIBBLE_TO_HEX[color.g >> 4]);
                builder.Append(NIBBLE_TO_HEX[color.g & 0xF]);
                builder.Append(NIBBLE_TO_HEX[color.b >> 4]);
                builder.Append(NIBBLE_TO_HEX[color.b & 0xF]);
                builder.Append("00>");
                builder.Append(text[i]);
                builder.Append("</color>");
            }

            // Each frame, update the alpha values along the fading frontier.
            float fadingProgress = 0f;
            int opaqueChars = -1;
            while (opaqueChars < length - 1)
            {
                yield return null;

                fadingProgress += Time.deltaTime;

                float leadingEdge = fadingProgress * _travelSpeed;

                int lastChar = Mathf.Min(length - 1, Mathf.FloorToInt(leadingEdge));

                int newOpaque = opaqueChars;

                for (int i = lastChar; i > opaqueChars; i--)
                {
                    byte fade = (byte)(255f * Mathf.Clamp01((leadingEdge - i) / (_travelSpeed * _fadeInDuration)));
                    builder[i * 26 + 14] = NIBBLE_TO_HEX[fade >> 4];
                    builder[i * 26 + 15] = NIBBLE_TO_HEX[fade & 0xF];

                    if (fade == 255)
                        newOpaque = Mathf.Max(newOpaque, i);
                }

                opaqueChars = newOpaque;

                // This allocates a new string.
                narratorText.NarratorTMPText.text = builder.ToString();
            }
            narratorText.NarratorTMPText.text = text;
            _fade = null;
        }
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject && !_triggered)
        {
            _triggered = true;
            FadeTo();
        }
    }
}
