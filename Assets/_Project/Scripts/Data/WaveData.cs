using System;
using UnityEngine;

namespace LastLineDefense.Data
{
    [Serializable]
    public class EnemyGroup
    {
        public EnemyData enemyData;
        public int count = 8;
        public float spawnInterval = 1.0f;
    }

    [CreateAssetMenu(fileName = "NewWaveData", menuName = "Defense/Wave Data")]
    public class WaveData : ScriptableObject
    {
        public EnemyGroup[] enemyGroups;
        public float preWaveDelay = 3f;
    }
}
