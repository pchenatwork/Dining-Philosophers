using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Philosopher
{
    public interface IOutputter
    {
        void WriteLine(string s);
    }


    public class JsonOutputter : IOutputter
    {
        private JArray array = new JArray();
        public void WriteLine(string s)
        {
            array.Add(new JValue(s));
        }

        public string GetJSON()
        {
            return array.ToString();
        }
    }
}
