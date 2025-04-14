using UnityEngine.UI;
using UnityEngine;
using System;

public class ValueBar : MonoBehaviour {

    [SerializeField] private Image _lineBar;

    public void SetValue(float fillAmount) {
        MathF.Round(fillAmount, 2);
        _lineBar.fillAmount = fillAmount;
    }
}
