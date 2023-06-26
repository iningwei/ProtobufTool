using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Entity
{
    public class ProtoEntity
    {
        public bool isEnum = false;
        public string msgId = "";//消息ID，为enum的时候用不到； 
        public bool isC2SMsg = false;//当msgId不为""时才有意义。为true表示上行消息，为false表示下行消息
        public string msgAnotation = "";//消息注释

        public List<string> allLines = new List<string>();
        public string entityName;
        public void AddLine(string content)
        {
            allLines.Add(content);
        }


    }
}
