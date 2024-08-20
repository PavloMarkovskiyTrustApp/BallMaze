using Assets.Scripts.Systems.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement;  // UI �������, ���� ������� ��������
    [SerializeField] private float upperY;  // ������ Y ����������
    [SerializeField] private float lowerY;  // ����� Y ����������
    [SerializeField] private float initialSpeed = 10000f;  // ��������� ��������
    [SerializeField] private float deceleration = 500f;  // �������� ���������

    [SerializeField] private TMP_Text _lastBonus;
    [SerializeField] private TMP_Text _firstBonus;


    public event Action<int> OnBonusReceived;

    private float speed;  // ������� ��������

    private bool _rotate;

    private int receivedBonus;

    public void Rotate(int bonusAmount)
    {
        // �����������
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
            // ���� �������� ��� 0 � ��'��� �� ����� �����, ��������� ���
            if (speed <= 0 && Mathf.Approximately(uiElement.anchoredPosition.y, lowerY))
            {
                return;
            }

            // ���������� ���� �������
            float newY = uiElement.anchoredPosition.y - speed * Time.deltaTime;

            // �������� �� ���������� ������ �����
            if (newY <= lowerY)
            {
                newY = upperY;  // ����������� ��'��� � ������ �����
                speed -= deceleration;  // �������� ��������

                // ���� �������� ��� 0, ��������� ��'��� � ����� �����
                if (speed <= 0)
                {
                    newY = lowerY;
                    speed = 0;  // Գ����� �������� �� 0
                    _rotate = false;
                    Invoke("ReceiveBonus", 1);
                    
                }
            }

            // ����������� ���� �������
            uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, newY);

        }
    }
    private void ReceiveBonus()
    {

        ResourcesBank.Instance.ModifyResource(ResourceTypes.Coins, receivedBonus);
        OnBonusReceived?.Invoke(receivedBonus);
    }
}
