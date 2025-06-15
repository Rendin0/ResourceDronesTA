
using R3;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalisableText : MonoBehaviour
{
    private TMP_Text _text;

    [SerializeField] private SerializableDictionary<Localisation, string> _localisedText;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        LocalisationManager.Instance.CurrentLocalisation.Subscribe(OnLocalisationChanged).AddTo(this);
    }

    private void OnLocalisationChanged(Localisation localisation)
    {
        _text.text = _localisedText[localisation];
    }
}