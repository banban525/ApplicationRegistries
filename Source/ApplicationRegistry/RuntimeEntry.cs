using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationRegistries
{
    class RuntimeEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly Func<string> _getValueFunc;
        public RuntimeEntry(EntryDefine define, Func<string> getValueFunc)
        {
            _define = define;
            _getValueFunc = getValueFunc;
        }
        public EntryDefine Define
        {
            get { return _define; }
        }

        public string GetValue()
        {
            return _getValueFunc();
        }

        public bool ExistsValue()
        {
            return true;
        }

        public IEntry Repace(string from, string to)
        {
            return new RuntimeEntry(_define.Replace(from,to), _getValueFunc);
        }
    }
}
