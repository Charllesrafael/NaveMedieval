using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class GameStateController : MonoBehaviour
    {
        public static GameStateController instance;
        public Dictionary<string,Action> eventStates;
        public GlobalVariables.States[] states;

        void Awake()
        {
            instance = this;
            eventStates = new Dictionary<string,Action>();

            foreach (var item in states)
                eventStates.Add(item.ToString(), null);
        }

        public void CallState(string state)
        {
            if(eventStates.ContainsKey(state))
                eventStates[state]?.Invoke();
        }
        

        public void SubcribState(string state, Action action)
        {
            if(eventStates.ContainsKey(state))
                eventStates[state] += action;
        } 

        public void UnsubcribState(string state, Action action)
        {
            if(eventStates.ContainsKey(state))
                eventStates[state] -= action;
        }   
    }
}
