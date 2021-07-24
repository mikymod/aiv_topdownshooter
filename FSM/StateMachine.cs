using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV.FSM
{
    public class StateMachine
    {
        public GameObject Owner { get; }

        private Dictionary<String, State> states;
        private State current;
        private bool initialized;

        public StateMachine(GameObject owner)
        {
            this.Owner = owner;
            states = new Dictionary<string, State>();
            initialized = false;
        }

        public void AddState(String key, State state) => states[key] = state;
        

        public void ChangeState(String key)
        {
            current?.OnExit();
            current = states[key];
            current.OnEnter();
        }

        public void SetInitialState(String key)
        {
            current = states[key];
            initialized = true;
        }

        public void Update()
        {
            if (!initialized)
            {
                return;
            }

            current.OnLogic();
        }
    }
}
