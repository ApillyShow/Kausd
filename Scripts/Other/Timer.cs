using UnityEngine;
using TMPro;
using System.Collections;
using System;
public class Timer : MonoBehaviour {
    [SerializeField] private int delta = 0;
    public event EventHandler OnBossTime;
    private TMP_Text _timerText;
    public int sec = 0;
    private int min = 0;

    private void Awake() {
        _timerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
    }

    private void Start() {
        StartCoroutine(Itimer());
    }

    private IEnumerator Itimer() {
        while (true) {
            if (sec == 59) {
                min++;
                sec = -1;
                OnBossTime?.Invoke(this, EventArgs.Empty);
            }
            sec += delta;
            _timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}
