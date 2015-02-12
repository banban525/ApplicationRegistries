using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $_output.Namespace$
{
    public static class $_output.ClassName$
    {
$_output.Entries.AllEntries:{entry|
        /// <summary>
        /// $entry.Define.Description$
        /// <summary>
        public static $entry.Define.Type$ $entry.Define.ID$
        {
            get
            {
                return ApplicationRegistries.ApplicationRegistry.GetValue<$entry.Define.Type$>("$entry.Define.ID$");
            \}
        \}
}$
    }
}