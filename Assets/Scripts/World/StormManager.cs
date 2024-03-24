using UnityEngine;

namespace World
{
    public class StormManager : MonoBehaviour
    {
        [SerializeField] [Min(1f)] private float cycleLengthSeconds = 60f;
        [SerializeField] [Min(1f)] private float durationSeconds = 10f;
        [SerializeField] [Min(0f)] private float vignetteUpdateSpeed = 1f;
        
        [SerializeField] private CanvasGroup vignetteCanvasGroup;
        [SerializeField] private ParticleSystem particles;

        private float _tickTimerSeconds;

        private void Start()
        {
            var isStormActive = IsStormActive();
            vignetteCanvasGroup.alpha = isStormActive ? 1f : 0f;
            if (isStormActive)
            {
                particles.Simulate(particles.main.startLifetime.constant);
            }
        }
        private void Update()
        {
            var isStormActive = IsStormActive();

            var targetVignetteAlpha = isStormActive ? 1f : 0f;
            vignetteCanvasGroup.alpha =
                Mathf.MoveTowards(vignetteCanvasGroup.alpha, targetVignetteAlpha, vignetteUpdateSpeed * Time.deltaTime);
            
            switch (isStormActive)
            {
                case false when particles.isPlaying:
                    particles.Stop();
                    break;
                case true when !particles.isPlaying:
                    particles.Play();
                    break;
            }
        }

        private bool IsStormActive()
        {
            var cycleTime = GameManager.Instance.WorldTime % cycleLengthSeconds;
            return cycleTime < durationSeconds && GameManager.Instance.WorldTime > cycleLengthSeconds;
        }
    }
}
