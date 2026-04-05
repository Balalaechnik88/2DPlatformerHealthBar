using System.Collections;
using UnityEngine;

public class HealthSmoothBarIndicator : HealthBarIndicator
{
    [SerializeField] private float _smoothTime = 0.25f;

    private Coroutine _smoothRoutine;

    protected override void Apply(int currentHealth, int maxHealth)
    {
        if (_slider == null)
            return;

        float target = GetNormalized(currentHealth, maxHealth);

        if (_smoothRoutine != null)
            StopCoroutine(_smoothRoutine);

        _smoothRoutine = StartCoroutine(SmoothTo(target));
    }

    private IEnumerator SmoothTo(float target)
    {
        float start = _slider.value;
        float time = 0f;

        if (_smoothTime <= 0f)
        {
            _slider.value = target;
            _smoothRoutine = null;
            yield break;
        }

        while (time < _smoothTime)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / _smoothTime);
            _slider.value = Mathf.Lerp(start, target, t);

            yield return null;
        }

        _slider.value = target;
        _smoothRoutine = null;
    }
}
