using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class ValueSystem {
    [SerializeField] private ValueEvent _ValueChanged = new();
    public float _value;
    private float _fillAmount;
    public float _valueMax;

    public void Setup(float value) {
        _value = _valueMax = value;
        SayChanged();
    }
    
    public void AddValue(float value) {
        _value += value;
        if (_value > _valueMax) _value = _valueMax;
        SayChanged();
    }
    
    public void AddValueMax(float value) {
       _valueMax *= value;
       _valueMax = MathF.Round(_valueMax, 2);
        SayChanged();
    }
    public void RemoveValue(float value) {
        _value -= value;
        if (_value < 0) _value = 0;
        
        SayChanged();
    }

    private void SayChanged() {
        _fillAmount = _value / _valueMax;
        _ValueChanged.Invoke(_fillAmount);
    }
    
    [System.Serializable]
    public class ValueEvent : UnityEvent<float>{}
}
