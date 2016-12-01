using System;
using System.Collections.Generic;

namespace Assets.LogicSystem
{
    public class Events
    {
        private static Events _instance;

        public static Events Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Events();
                return _instance;
            }
        }

        private Dictionary<string, MyEvent> _eventsMap = new Dictionary<string, MyEvent>();
        public delegate void MyEvent(object e);


        private Events()
        {

        }

        public void RegisterForEvent(string name, MyEvent function)
        {

            MyEvent evt = null;
            if (_eventsMap.TryGetValue(name, out evt))
            {
                _eventsMap[name] += function;
            }
            else
            {
                _eventsMap.Add(name, function);
                //Debug.Log("Event " + name + " created");
            }
        }

        public void RegisterForMultipleEvents(string[] names, MyEvent function)
        {
            foreach (string name in names)
            {
                RegisterForEvent(name, function);
            }
        }

        public void UnregisterForEvent(string name, MyEvent function)
        {
            if (_eventsMap.ContainsKey(name))
            {
                _eventsMap[name] -= function;
            }
        }

        public void DispatchEvent(string name, object parameter)
        {
            if (_eventsMap.ContainsKey(name))
                _eventsMap[name](parameter);
        }

        public void Reset()
        {

            _eventsMap.Clear();
        }

    }
}
