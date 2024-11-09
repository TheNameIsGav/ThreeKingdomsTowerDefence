using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Basic GameManager that controls state, and includes methods for instantiating the map, etc.
/// </summary>
public class GameManager : MonoBehaviour {

    public GameManager GlobalGameManager => this;

    private void Awake() {
        LoadSave();
    }

    /// <summary>
    /// Where Save Data will be loaded
    /// </summary>
    private void LoadSave() {
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void SaveGame() {

    }

    private float _fireFactionPower = 0.0f;
    private float _waterFactionPower = 0.0f;
    private float _EarthFactionPower = 0.0f;

    private State _state = State.GameStart;
    private State _previousState = State.GameStart;
    public State CurrentState => _state;

    private List<(State, State)> _validStatePairs = new List<(State, State)>() {
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
    public bool StateEnter(State stateToEnter) { 
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
    private bool StateExit(State stateBeingExited) {
        _previousState = _state;
        OnStateExited.Invoke(_previousState);
        return false;
    }

    public delegate void StateEntered(State state);
    public delegate void StateExited(State state);
    public event StateEntered OnStateEntered;
    public event StateExited OnStateExited;


}

public enum State {
    GameStart,
    HomeScreen,
    WarMap,
    InBattle,
    InPause,
    GameEscScreenOpen
}
