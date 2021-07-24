using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV.FSM
{
    public abstract class State
    {
        StateMachine sm;

        public State(StateMachine sm)
        {
            this.sm = sm;
        }

        public virtual void OnEnter() { }
        public virtual void OnLogic() { }
        public virtual void OnExit() { }
        
    }
}
