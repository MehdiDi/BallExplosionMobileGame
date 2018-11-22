using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class LevelDifficulty : MonoBehaviour
    {
        

        public int[] ChangeDifficultyTime;
        private EnemySpawner _spawner;
        private float speedIncrease;
        private float _changeDifficultyTimer = 0;

        private void Start()
        {
            speedIncrease = 0;
            _spawner = GameObject.Find("EnemyHandler").GetComponent<EnemySpawner>();
        }


        private BlockTypes.BlockColors _currentColor;

        public enum Difficulty
        {
            easy = 0,
            medium = 1 ,
            medium_hard = 2,
            hard = 3,
            very_hard = 4
        }
        [SerializeField]
        private Difficulty _currentDifficulty;

        internal Difficulty CurrentDifficulty
        {
            get
            {
                return _currentDifficulty;
            }

            set
            {
                _currentDifficulty = value;
            }
        }

        private void Update()
        {
            
            HandleDifficulty();

        }



        public void SetDifficulty(Difficulty diff)
        {
                CurrentDifficulty = diff;
        }
       
        
        
        private void HandleDifficulty()
        {
            _changeDifficultyTimer += Time.deltaTime;
            

            speedIncrease += Time.deltaTime / 100000;

            _spawner.SetSpeed(_spawner.blockSpeed + speedIncrease);

            switch (CurrentDifficulty)
            {
                case Difficulty.easy:
                    if (_changeDifficultyTimer > ChangeDifficultyTime[0])
                    {
                        _spawner.AddToMaxEnemies(1);
                        SetDifficulty(Difficulty.medium);
                    }
                    
                    break;
                case Difficulty.medium:
                    if (_changeDifficultyTimer > ChangeDifficultyTime[1])
                    {
                        _spawner.AddToMaxEnemies(2);
                        _spawner.substractFromSpawnTimer(0.3f);
                        SetDifficulty(Difficulty.medium_hard);

                    }
                    
                    break;
                        case Difficulty.medium_hard:
                    if (_changeDifficultyTimer > ChangeDifficultyTime[2])
                    {
                        _spawner.AddToMaxEnemies(1);
                        _spawner.substractFromSpawnTimer(0.3f);
                        SetDifficulty(Difficulty.hard);

                    }
                    break;
                case Difficulty.hard:
                    if (_changeDifficultyTimer > ChangeDifficultyTime[3])
                    {
                        _spawner.substractFromSpawnTimer(0.5f);
                        _spawner.AddToMaxEnemies(2);
                        SetDifficulty(Difficulty.very_hard);
                    }

                    
                    break;
                case Difficulty.very_hard:
                    break;
            }
        }
    }
    
}
