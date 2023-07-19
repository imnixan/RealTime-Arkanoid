using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{
    public event UnityAction NullHP;

    [SerializeField]
    private Image hpIndication;

    private const float MaxHP = 1;
    private const float Damage = 0.34f;
    private float currentHp;

    private void Start()
    {
        currentHp = MaxHP;
        hpIndication.fillAmount = currentHp;
    }

    private void OnMeteoritFall()
    {
        if (currentHp > 0)
        {
            currentHp -= Damage;
            StopAllCoroutines();
            StartCoroutine(ReduceHpAnim());
            if (currentHp <= 0)
            {
                NullHP?.Invoke();
            }
        }
    }

    private IEnumerator ReduceHpAnim()
    {
        for (float i = hpIndication.fillAmount; i > currentHp; i -= 0.01f)
        {
            hpIndication.fillAmount = i;
            yield return new WaitForFixedUpdate();
        }
    }
}
