using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core
{
    public class Setting
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SettingScope Scope { get; set; }

    }

    public class Setting<TValue> : Setting
    {
        public new TValue Value { get; set; }

    }
    public enum SettingScope
    {
        Application = 0,
        User
    }
}
