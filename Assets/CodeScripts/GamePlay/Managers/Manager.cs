using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    protected BgManager bgManager;
    protected UIManager uiManager;
    protected GameplayManager gm;

    protected void OnEnable()
    {
        bgManager = GameObject.FindWithTag("BGManager").GetComponent<BgManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameplayManager>();
        Subscribe();
    }

    protected void OnDisable()
    {
        Unsubscribe();
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}
