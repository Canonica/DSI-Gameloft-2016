using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class XInput : MonoBehaviour
{
    public static XInput instance = null;
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Set vibration according to triggers
        //GamePad.SetVibration(playerIndex, vibe.x, vibe.y);

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }

    public void useVibe(int id, float time, float force1, float force2)
    {
        StartCoroutine(vibration((PlayerIndex)(id), time,  force1,  force2));
    }

    public float getTrigger(int id)
    {
        return GamePad.GetState((PlayerIndex)(id - 1)).Triggers.Right;
    }

    public ButtonState getButton(int id, char bt)
    {
        
        switch (bt)
        {
            case 'A':
                return GamePad.GetState((PlayerIndex)(id - 1)).Buttons.A;
            case 'B':
                return GamePad.GetState((PlayerIndex)(id - 1)).Buttons.B;
                
            case 'X':
                return GamePad.GetState((PlayerIndex)(id - 1)).Buttons.X;
                
            case 'Y':
                return GamePad.GetState((PlayerIndex)(id - 1)).Buttons.Y;
                
            default:
                Debug.Log("ERROR X INPUT");
                return new ButtonState();
        }
        
    }

    public float getYStick(int id)
    {
       return GamePad.GetState((PlayerIndex)(id - 1)).ThumbSticks.Left.Y;
    }

    IEnumerator vibration(PlayerIndex id, float time, float force1, float force2)
    {
        GamePad.SetVibration(id, force1, force2);
        yield return new WaitForSeconds(time);
        GamePad.SetVibration(id, 0, 0);
    }
}
