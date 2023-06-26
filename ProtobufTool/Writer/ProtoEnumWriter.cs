using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Writer
{
    internal class ProtoEnumWriter
    {
        public static Dictionary<string, bool> enumDic = new Dictionary<string, bool>();

        public static void Write(List<ProtoEntity> protoEntities, string outputPath)
        {
            int lineCount = 0;
            for (int i = 0; i < protoEntities.Count; i++)
            {
                var entity = protoEntities[i];
                if (entity.isEnum)
                {
                    lineCount += entity.allLines.Count;
                }
            }


            lineCount += 2;//前面
            lineCount += 2;//后面

            string[] alllines = new string[lineCount];
            alllines[0] = "---@class ProtoEnum";
            alllines[1] = "local ProtoEnum={";

            int index = 2;
            for (int i = 0; i < protoEntities.Count; i++)
            {
                var entity = protoEntities[i];
                if (entity.isEnum)
                {
                    for (int j = 0; j < entity.allLines.Count; j++)
                    {
                        var line = entity.allLines[j];
                        if (line.StartsWith("enum") && line.StartsWith("enumeration") == false)
                        {
                            Console.WriteLine("write enum:::" + line);
                            //去除enum符号
                            line = line.Remove(0, 4).Trim();
                            if (line.Contains("PbRoleRetResp"))
                            {
                                var a = 1;
                            }
                            //枚举名称后面要添加=
                            int enumNameEndIndex = line.IndexOf(' ');
                            if (enumNameEndIndex == -1)
                            {
                                //情形1， 结构如：enum xxx{
                                //情形2， 结构如：enum xxx
                                if (line.Contains("{"))
                                {
                                    enumNameEndIndex = line.IndexOf("{");
                                }
                                else
                                {
                                    enumNameEndIndex = line.Length;
                                }
                            }
                            string enumName = line.Substring(0, enumNameEndIndex).Trim();
                            if (enumDic.ContainsKey(enumName))
                            {
                                Console.WriteLine("duplicated enum:" + enumName);
                                throw new Exception("duplicated enum:" + enumName);
                            }
                            else
                            {
                                enumDic.Add(enumName, true);
                            }
                            line = line.Insert(enumNameEndIndex, "=");

                        }
                        line = line.Replace("//", "--");
                        if (j == entity.allLines.Count - 1)
                        {
                            line += ",";
                        }
                        alllines[index] = line;
                        index++;
                    }
                }
            }

            alllines[lineCount - 2] = "}";
            alllines[lineCount - 1] = "return ProtoEnum";

            File.WriteAllLines(outputPath, alllines);
            Console.WriteLine("write ProtoEnum lua success:" + outputPath);
        }
    }
}
