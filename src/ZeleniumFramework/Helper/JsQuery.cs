using System;
using System.Text.RegularExpressions;
using ZeleniumFramework.Config;

namespace ZeleniumFramework.Helper
{
    public class JsQuery
    {
        private string script;
        public string Name { get; set; }
        public string Script
        {
            get
            {
                if (this.Parameters == null)
                {
                    return this.script;
                }

                var script = this.script;
                for (var index = 0; index < this.Parameters.Length; index++)
                {
                    var regex = new Regex(Regex.Escape($"{{{index}}}"));
                    script = regex.Replace(script, this.Parameters[index], 1);
                }
                return script;

            }
            set => this.script = value;
        }

        public TimeSpan Timeout { get; set; } = TimeConfig.DefaultTimeout;
        public string[] Parameters { get; set; }

        public JsQuery SetParameters(params string[] parameters)
        {
            this.Parameters = parameters;
            return this;
        }
    }
}
