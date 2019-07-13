using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace famaCodeWindow.dataModels
{

    public class Model
    {
        public enum modelType
        {
            Action,
            Player,
            Term,
            Moment,
            Pass,
            Zone,
        }
        public string id;
        public List<string> incompatibleBNTS;
        public System.Windows.Controls.Button assingedBTN;
        public modelType type;
    }
    public class Action : Model
    {
        public string text;
        
        public Action(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Action;
            this.text = text;
        }
    }

    public class Term : Model
    {
        public string text;

        public Term(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Term;
            this.text = text;
        }
    }


    
}
