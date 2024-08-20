using Assets.Scripts.Systems.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement;  // UI елемент, який потрібно анімувати
    [SerializeField] private float upperY;  // Верхня Y координата
    [SerializeField] private float lowerY;  // Нижня Y координата
    [SerializeField] private float initialSpeed = 10000f;  // Початкова швидкість
    [SerializeField] private float deceleration = 500f;  // Швидкість зменшення

    [SerializeField] private TMP_Text _lastBonus;
    [SerializeField] private TMP_Text _firstBonus;


    public event Action<int> OnBonusReceived;

    private float speed;  // Поточна швидкість

    private bool _rotate;

    private int receivedBonus;

    public void Rotate(int bonusAmount)
    {
        // Ініціалізація
        speed = initialSpeed;
        uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, upperY);

        _rotate = true;
        receivedBonus = bonusAmount;
        _lastBonus.text = bonusAmount.ToString();
        _firstBonus.text = bonusAmount.ToString();
    }

    private void Update()
    {
        if (_rotate)
        {
            // Якщо швидкість стає 0 і об'єкт на нижній точці, зупиняємо рух
            if (speed <= 0 && Mathf.Approximately(uiElement.anchoredPosition.y, lowerY))
            {
                return;
            }

            // Обчислюємо нову позицію
            float newY = uiElement.anchoredPosition.y - speed * Time.deltaTime;

            // Перевірка на досягнення нижньої точки
            if (newY <= lowerY)
            {
                newY = upperY;  // Телепортуємо об'єкт у верхню точку
                speed -= deceleration;  // Зменшуємо швидкість

                // Якщо швидкість стає 0, зупиняємо об'єкт у нижній точці
                if (speed <= 0)
                {
                    newY = lowerY;
                    speed = 0;  // Фіксуємо швидкість на 0
                    _rotate = false;
                    Invoke("ReceiveBonus", 1);
                    
                }
            }

            // Застосовуємо нову позицію
            uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, newY);

        }
    }
    private void ReceiveBonus()
    {

        ResourcesBank.Instance.ModifyResource(ResourceTypes.Coins, receivedBonus);
        OnBonusReceived?.Invoke(receivedBonus);
    }
}
