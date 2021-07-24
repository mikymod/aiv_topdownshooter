using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooterAIV.FSM
{
    public interface IState
    {
        void OnEnter();
        void OnLogic();
        void OnDraw();
        void OnExit();
    }
}
