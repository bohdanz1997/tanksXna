using System;
using System.Collections.Generic;

namespace Tanks.Tools
{
    class Sheduler
    {
        List<Func<bool>> tasks;

        public Sheduler()
        {
            tasks = new List<Func<bool>>();
        }

        public void Add(Func<bool> task)
        {
            tasks.Add(task);
        }

        public void Update()
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i] != null && tasks[i]())
                {
                    tasks.Remove(tasks[i]);
                }
            }
        }
    }
}
