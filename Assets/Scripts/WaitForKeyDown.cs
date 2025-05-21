using UnityEngine;

public class WaitForKeyDown : CustomYieldInstruction
{
    private KeyCode _key;
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKeyDown(_key);
        }
    }

    public WaitForKeyDown(KeyCode key)
    {
        _key = key;
    }
}