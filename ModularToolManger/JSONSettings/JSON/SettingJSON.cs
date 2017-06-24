using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSettings
{
    internal class SettingJSON
    {
        public List<SettingsNode> nodes;

        public SettingJSON()
        {
            nodes = new List<SettingsNode>();
        }

        public bool AddNew(string Name)
        {
            if (Contained(Name))
                return false;
            nodes.Add(new SettingsNode(Name));
            return true;
        }

        public bool AddValue(string Name, string Key, object Value)
        {
            SettingsNode node = getNode(Name);
            if (node == null)
                return false;
            node.AddOrChangeKeyValue(Key, Value);
            return true;
        }

        public string GetValue(string Name, string Key, out SettingsType type)
        {
            SettingsNode node = getNode(Name);
            if (node == null)
            {
                type = SettingsType.Error;
                return String.Empty;
            }
            string Value = node.GetKeyValue(Key, out type);

            return Value;
        }

        private SettingsNode getNode(string Name)
        {
            foreach (SettingsNode node in nodes)
            {
                if (node.Name == Name)
                    return node;
            }
            return null;
        }

        private bool Contained(string name)
        {
            foreach (SettingsNode node in nodes)
            {
                if (node.Name == name)
                    return true;
            }
            return false;
        }
    }
}
