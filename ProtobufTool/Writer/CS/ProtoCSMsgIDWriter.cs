using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufTool.Writer.CS
{
    internal class ProtoCSMsgIDWriter
    {
        static List<string> allLines = new List<string>();
        public static void Write(List<ProtoEntity> protoEntities, string outputPath)
        {
            var c2sLines = getC2SLines(protoEntities);
            var s2cLines = getS2CLines(protoEntities);

            //引入Ext表
            allLines.Add("public enum ProtobufMsgID");
            allLines.Add("{");
            allLines.Add("");

            //上行部分
            allLines.Add("    //---------->上行");
            for (int i = 0; i < c2sLines.Count; i++)
            {
                allLines.Add("    " + c2sLines[i]);
            }
            allLines.Add("");


            //下行部分
            allLines.Add("    //---------->下行");

            for (int i = 0; i < s2cLines.Count; i++)
            {
                allLines.Add("    " + s2cLines[i]);
            }
            allLines.Add("}");


            File.WriteAllLines(outputPath, allLines.ToArray());

            Console.WriteLine("write ProtoMsgID CS success:" + outputPath);
        }

        static List<string> getC2SLines(List<ProtoEntity> allEntities)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < allEntities.Count; i++)
            {
                var entity = allEntities[i];
                if (entity.isEnum == false && entity.msgId != "" && entity.isC2SMsg)
                {
                    string anotationStr = entity.msgAnotation;
                    if (string.IsNullOrEmpty(anotationStr) == false)
                    {
                        lines.Add(anotationStr);
                    }

                    string contentStr1 = "[ProtobufMsgIDDes(\"" + entity.entityName + "\"," + entity.msgId + ")]";
                    string contentStr2 = entity.entityName + ",";
                    lines.Add(contentStr1);
                    lines.Add(contentStr2);
                    lines.Add("");//换行
                }

            }

            return lines;
        }

        static List<string> getS2CLines(List<ProtoEntity> allEntities)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < allEntities.Count; i++)
            {
                var entity = allEntities[i];
                if (entity.isEnum == false && entity.msgId != "" && entity.isC2SMsg == false)
                {
                    string anotationStr = entity.msgAnotation;
                    if (string.IsNullOrEmpty(anotationStr) == false)
                    {
                        lines.Add(anotationStr);
                    }

                    string contentStr1 = "[ProtobufMsgIDDes(\"" + entity.entityName + "\"," + entity.msgId + ")]";
                    string contentStr2 = entity.entityName + ",";
                    lines.Add(contentStr1);
                    lines.Add(contentStr2);
                    lines.Add("");//换行
                }

            }

            return lines;
        }
    }
}