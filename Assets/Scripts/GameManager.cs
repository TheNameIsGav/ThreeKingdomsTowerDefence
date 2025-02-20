using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Basic GameManager that controls state, and includes methods for instantiating the map, etc.
/// </summary>
public static class GameManager {

    //public GameManager GlobalGameManager => this;
    public static NodeManager CurrentNodeManager = null;

    //private void Awake() {
    //    LoadSave();
    //    StateEnter(State.InBattle); //TESTING ONLY - CHANGE LATER
    //}

    /// <summary>
    /// Where Save Data will be loaded
    /// </summary>
    private static void LoadSave() {
        
    }

    /// <summary>
    /// 
    /// </summary>
    private static void SaveGame() {

    }

    private static float _fireFactionPower = 0.0f;
    private static float _waterFactionPower = 0.0f;
    private static float _EarthFactionPower = 0.0f;

    private static State _state = State.InBattle; //Testing Purposes only, change later
    private static State _previousState = State.GameStart;
    public static State CurrentState => _state;

    private static List<(State, State)> _validStatePairs = new List<(State, State)>() {
        (State.GameStart, State.HomeScreen),
        (State.HomeScreen, State.WarMap),
        (State.WarMap, State.InPause),
        (State.InPause, State.InBattle),
        (State.InBattle, State.InPause),

        (State.GameStart, State.GameEscScreenOpen),
        (State.HomeScreen, State.GameEscScreenOpen),
        (State.WarMap, State.GameEscScreenOpen),
        (State.InPause, State.GameEscScreenOpen),
        (State.InBattle, State.GameEscScreenOpen),
        (State.GameEscScreenOpen, State.GameStart),
        (State.GameEscScreenOpen, State.HomeScreen),
        (State.GameEscScreenOpen, State.WarMap),
        (State.GameEscScreenOpen, State.InPause),
        (State.GameEscScreenOpen, State.InBattle),

        (State.WarMap, State.HomeScreen)
        
        
    
    };

    /// <summary>
    /// Accepts a state, transitions to that state, then returns whether the transition was successful. 
    /// </summary>
    /// <returns>Returns ture if state exited correctly</returns>
    public static bool StateEnter(State stateToEnter) { 
        (State, State) statePair = (CurrentState, stateToEnter);
        if (_validStatePairs.Contains(statePair)) {
            StateExit(_state);

            _state = stateToEnter;
            OnStateEntered.Invoke(_state);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Private Method used to exit a state. Should not be called outside of GameManager.
    /// </summary>
    /// <returns>Returns true if state exited correctly</returns>
    private static bool StateExit(State stateBeingExited) {
        _previousState = _state;
        OnStateExited.Invoke(_previousState);
        return false;
    }

    public delegate void StateEntered(State state);
    public delegate void StateExited(State state);
    public static event StateEntered OnStateEntered;
    public static event StateExited OnStateExited;


}

public enum State {
    GameStart,
    HomeScreen,
    WarMap,
    InBattle,
    InPause,
    GameEscScreenOpen
}
