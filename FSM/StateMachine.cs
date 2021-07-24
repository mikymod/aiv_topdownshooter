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

        private Dictionary<String, IState> states;
        private IState current;
        private bool initialized;

        public StateMachine(GameObject owner)
        {
            this.Owner = owner;
            states = new Dictionary<string, IState>();
            initialized = false;
        }

        public void AddState(String key, IState state) => states[key] = state;
        

        public void ChangeState(String key)
        {
            current?.OnExit();
            current = states[key];
            current.OnEnter();
        }

        public void SetInitialState(String key)
        {
            initialized = true;
            current = states[key];
            current.OnEnter();
        }

        public void Update()
        {
            if (!initialized)
            {
                return;
            }

            current.OnLogic();
        }

        public void Draw()
        {
            if (!initialized)
            {
                return;
            }

            current.OnDraw();
        }
    }
}
