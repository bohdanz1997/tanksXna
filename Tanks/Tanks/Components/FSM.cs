using Ash.FSM;
using System.Collections.Generic;

namespace Tanks.Components
{
    class FSM
    {
        public Dictionary<string, EntityStateMachine> list;
        
        public FSM(EntityStateMachine item)
        {
            list = new Dictionary<string, EntityStateMachine>();
            list.Add("Default", item);
        }

        public FSM(Dictionary<string, EntityStateMachine> list)
        {
            this.list = list;
        }
    }
}
