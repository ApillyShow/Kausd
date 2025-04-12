using System;
using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    [SerializeField] private Transform _Player;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _enemyBoss;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _enemiesToSpawn;


    private void Start() {
        StartCoroutine(SpawnEnemies());
        _timer.OnBossTime += _timer_OnBossTime;
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            for (int i = 0; i < _enemiesToSpawn; i++) {
                EnemySpawn();
            }
            yield return new WaitForSeconds(_spawnInterval);
            _spawnIntervalTime();
            IncreaseEnemeyCount();
        }
    }

    private void _spawnIntervalTime() {
        _spawnInterval += 0.5f;
    }
    private void EnemySpawn() {
        int randomIndex = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        GameObject selectionEnemy = _enemyPrefabs[randomIndex];

        Vector2 spawnPosition = (Vector2)_Player.position + UnityEngine.Random.insideUnitCircle * _spawnRadius;
        Instantiate(selectionEnemy, spawnPosition, Quaternion.identity);
    }

    private void IncreaseEnemeyCount() {
        if (_enemiesToSpawn < 10) {
            _enemiesToSpawn++;
        } else if (_enemiesToSpawn == 10 && _spawnInterval >= 5) {
            _enemiesToSpawn += 10; 
        }
    }

    private void _timer_OnBossTime(object sender, EventArgs e) {
        Vector2 spawnPosition = (Vector2)_Player.position + UnityEngine.Random.insideUnitCircle * _spawnRadius;
        Instantiate(_enemyBoss, spawnPosition, Quaternion.identity);
    }
}
