using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterTmp;
    [SerializeField] private Animator animator;

    [Header("Coin accent settings")]
    [SerializeField] private Rotator coinRotator;

    public void RefreshCounter(int value)
    {
        counterTmp.text = value.ToString();
        animator.SetTrigger(Constants.animatorTrigger_collected);
    }

    public void ShowResult()
    {
        animator.SetTrigger(Constants.animatorTrigger_result);
    }
}
