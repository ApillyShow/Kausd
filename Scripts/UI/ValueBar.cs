using UnityEngine.UI;
using UnityEngine;

public class ValueBar : MonoBehaviour {

    [SerializeField] private Image _lineBar;

    public void SetValue(float fillAmount) {
        _lineBar.fillAmount = fillAmount;
    }
}
