using Assets.Scripts.Systems.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class TextManager
{
    [SerializeField] private RectTransform _coinsBack;
    public void ModifyResource(TMP_Text text, int amount)
    {
        _coinsBack.anchoredPosition = new Vector2(0, _coinsBack.anchoredPosition.y);
        if (amount > 9)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 99)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 9999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x -= 60;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 99999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        text.text = FormatAmount(amount);
    }

    public void StartResourceSet(TMP_Text text)
    {
        int amount = SaveManager.LoadResource(ResourceTypes.Coins);
        _coinsBack.anchoredPosition = new Vector2(0, _coinsBack.anchoredPosition.y);


        if (amount > 9)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 99)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 9999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x -= 60;
            _coinsBack.anchoredPosition = currentPosition;

        }
        if (amount > 99999)
        {
            Vector2 currentPosition = _coinsBack.anchoredPosition;
            currentPosition.x += 30;
            _coinsBack.anchoredPosition = currentPosition;

        }
        text.text = FormatAmount(amount);

    }
    private string FormatAmount(int amount)
    {
        if (amount > 9999)
        {
            return (amount / 1000).ToString() + "k";
        }
        else
        {
            return amount.ToString();
        }
    }
}
