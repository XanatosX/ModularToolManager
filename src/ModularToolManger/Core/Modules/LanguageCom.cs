using PluginCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModularToolManger.Core.Modules
{
    public class LanguageCom : Module
    {
        public override void init()
        {
            addType("languageRequest");
            base.init();
        }

        public override void Notified(MessageData DataSet)
        {
            string returnVal = CentralLanguage.LanguageManager.GetText(DataSet.Data.ToString());
            SendMessage("LanguageRespond", returnVal);
        }

        public void LanguageChanged()
        {
            SendMessage("LanguageChanged", CentralLanguage.LanguageManager.Name);
        }
    }
}
