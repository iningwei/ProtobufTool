using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Writer
{
    internal class ProtoMsgIDWriter
    { 
        static List<string> allLines = new List<string>();
        public static void Write(List<ProtoEntity> protoEntities, string outputPath)
        {
            var c2sLines = getC2SLines(protoEntities);
            var s2cLines = getS2CLines(protoEntities);

            //引入Ext表
            allLines.Add("---@type ProtoMsgIDExt");
            allLines.Add("ProtoMsgIDExt=require \"ProtoMsgIDExt\"");
            allLines.Add("");

            //上行部分
            allLines.Add("--发送");
            allLines.Add("Proto.MsgIdByNameCS = {");
            for (int i = 0; i < c2sLines.Count; i++)
            {
                allLines.Add(c2sLines[i]);
            }
            allLines.Add("}");
            allLines.Add("");//换行

            //下行部分
            allLines.Add("--接收");
            allLines.Add("Proto.MsgIdByNameSC = {");
            for (int i = 0; i < s2cLines.Count; i++)
            {
                allLines.Add(s2cLines[i]);
            }
            allLines.Add("}");
            allLines.Add("");//换行

            //函数部分
            allLines.Add("Proto.MsgNameByIdCS={}");
            allLines.Add("for k, v in pairs(Proto.MsgIdByNameCS) do");
            allLines.Add("    Proto.MsgNameByIdCS[v] = k");
            allLines.Add("end");
            allLines.Add("");
            allLines.Add("Proto.MsgNameByIdSC = {}");
            allLines.Add("for k, v in pairs(Proto.MsgIdByNameSC) do");
            allLines.Add("    Proto.MsgNameByIdSC[v] = k");
            allLines.Add("end");
            allLines.Add("");//换行

            //metatable部分
            //
            allLines.Add("setmetatable(Proto.MsgIdByNameCS,{");
            allLines.Add("    __index=function(t,k)");
            allLines.Add("        local value=ProtoMsgIDExt.MsgIdByNameCS[k]");
            allLines.Add("        if value~=nil then");
            allLines.Add("            t[k]=value");
            allLines.Add("            return value");
            allLines.Add("        end");
            allLines.Add("    end");
            allLines.Add("})");
            allLines.Add("");
            //
            allLines.Add("setmetatable(Proto.MsgIdByNameSC,{");
            allLines.Add("    __index=function(t,k)");
            allLines.Add("        local value=ProtoMsgIDExt.MsgIdByNameSC[k]");
            allLines.Add("        if value~=nil then");
            allLines.Add("            t[k]=value");
            allLines.Add("            return value");
            allLines.Add("        end");
            allLines.Add("    end");
            allLines.Add("})");
            allLines.Add("");
            //
            allLines.Add("setmetatable(Proto.MsgNameByIdCS,{");
            allLines.Add("    __index=function(t,k)");
            allLines.Add("        local value=ProtoMsgIDExt.MsgNameByIdCS[k]");
            allLines.Add("        if value~=nil then");
            allLines.Add("            t[k]=value");
            allLines.Add("            return value");
            allLines.Add("        end");
            allLines.Add("    end");
            allLines.Add("})");
            allLines.Add("");
            //
            allLines.Add("setmetatable(Proto.MsgNameByIdSC,{");
            allLines.Add("    __index=function(t,k)");
            allLines.Add("        local value=ProtoMsgIDExt.MsgNameByIdSC[k]");
            allLines.Add("        if value~=nil then");
            allLines.Add("            t[k]=value");
            allLines.Add("            return value");
            allLines.Add("        end");
            allLines.Add("    end");
            allLines.Add("})");
            allLines.Add("");


            File.WriteAllLines(outputPath, allLines.ToArray());

            Console.WriteLine("write ProtoMsgID lua success:" + outputPath);
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
                        lines.Add(anotationStr.Replace("//", "--"));
                    }

                    string contentStr = "[\"" + entity.entityName + "\"]=\"" + entity.msgId + "\",";
                    lines.Add(contentStr);
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
                        lines.Add(anotationStr.Replace("//", "--"));
                    }

                    string contentStr = "[\"" + entity.entityName + "\"]=\"" + entity.msgId + "\",";
                    lines.Add(contentStr);
                    lines.Add("");//换行
                }

            }

            return lines;
        }
    }
}
