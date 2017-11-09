using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace LauncherV1._0
{
    [DelimitedRecord(";")]
    class Projects
    {
        private string prettyName;
        private string fullPath;
        private string name;
        private string projectPath;
        private string description;


        public Projects(string prettyName, string fullPath, string name, string projectPath, string description = "none")
        {
            this.prettyName = prettyName;
            this.fullPath = fullPath;
            this.name = name;
            this.projectPath = projectPath;
            this.description = description;
        }

        public Projects()
        {

        }

        public override string ToString()
        {
            return this.prettyName;
        }

        public string PrettyName
        {
            get
            {
                return this.prettyName;
            }
            set
            {
                this.prettyName = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string FullPath
        {
            get
            {
                return this.fullPath;
            }
            set
            {
                this.fullPath = value;
            }
        }

        public string ProjectPath
        {
            get
            {
                return this.projectPath;
            }
            set
            {
                this.projectPath = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
    }
}
