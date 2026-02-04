using System;
using UnityEngine;

    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private TimerManager timerManager;
        [SerializeField] private LeaderboardManager leaderboardManager;

        public event Action<GameState> StateChanged;

        public GameState CurrentState { get; private set; } = GameState.Idle;

        private void Awake()
        {
            if (timerManager != null)
            {
                timerManager.TimerEnded += HandleTimerEnded;
            }

            if (scoreManager != null)
            {
                scoreManager.SetScoringEnabled(false);
            }
        }

        private void OnDestroy()
        {
            if (timerManager != null)
            {
                timerManager.TimerEnded -= HandleTimerEnded;
            }
        }

        private void Update()
        {
            if (CurrentState != GameState.Running || timerManager == null)
            {
                return;
            }

            if (!timerManager.IsRunning && timerManager.RemainingSeconds <= 0f)
            {
                EndGame();
            }
        }

        public void StartGame()
        {
            if (CurrentState == GameState.Running)
            {
                return;
            }

            scoreManager?.ResetScore();
            scoreManager?.SetScoringEnabled(true);
            timerManager?.StartTimer();
            SetState(GameState.Running);
        }

        public void EndGame()
        {
            if (CurrentState == GameState.Ended)
            {
                return;
            }

            timerManager?.StopTimer();
            if (scoreManager != null)
            {
                leaderboardManager?.RecordScore(scoreManager.Score);
                scoreManager.ResetScore();
                scoreManager.SetScoringEnabled(false);
            }
            SetState(GameState.Ended);
        }

        private void HandleTimerEnded()
        {
            if (CurrentState == GameState.Running)
            {
                EndGame();
            }
        }

        private void SetState(GameState newState)
        {
            CurrentState = newState;
            StateChanged?.Invoke(CurrentState);
        }
    }