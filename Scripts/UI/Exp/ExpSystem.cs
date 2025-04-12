using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class ExpSystem { 
    [SerializeField] private ExpValueEvent _ExpValueChanged = new();

    public int _level;
    public int _expValue;
    private float _fillAmount;
    private int _expValueMax;

    private void Awake() {
        _expValue = 0;
        _expValueMax = 40 * _level;
        _level = 1;
    }
    public void AddExpValue(int expValue) {
        _expValue += expValue;
        if (_expValue > _expValueMax) {
            _expValue = 0;
            LvlUp();
            _expValueMax = 40 * _level;
        }
        SayChanged();
    }

    private void LvlUp() {
        _level++;
    }
    private void SayChanged() {
        _fillAmount = (float)_expValue / _expValueMax;
        _ExpValueChanged.Invoke(_fillAmount);
    }

    [System.Serializable]
    public class ExpValueEvent : UnityEvent<float>{}
}
