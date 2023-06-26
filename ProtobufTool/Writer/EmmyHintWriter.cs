using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Writer
{
    internal class EmmyHintWriter
    {
        public static void Write(List<ProtoEntity> allEntities, string outputPath)
        {
            List<string> allLines = new List<string>();
            for (int i = 0; i < allEntities.Count; i++)
            {
                var entity = allEntities[i];
                if (entity.isEnum == false)
                {
                    for (int j = 0; j < entity.allLines.Count; j++)
                    {
                        var line = entity.allLines[j];
                        line = line.Trim();
                        if (line == "")
                        {
                            continue;
                        }
                        if (line.StartsWith("//"))
                        {
                            continue;
                        }
                        else
                        {
                            string[] seps = line.Split(' ');
                            if (seps.Length < 2)
                            {
                                continue;
                            }

                            if (line.StartsWith("message"))
                            {
                                //类注释
                                string classAnotationStr = "---@class " + entity.entityName;

                                allLines.Add(classAnotationStr);
                            }
                            else
                            {
                                //field注释
                                int fieldTypeIndexInSeps = 0;
                                string fieldType = "";
                                string annotation = "";
                                if (line.Contains(@"//"))
                                {
                                  annotation=" @"+line.Substring(line.IndexOf(@"//")+2);                                     
                                }
                                if (line.Contains("repeated"))
                                {

                                    for (int k = 1; k < seps.Length; k++)
                                    {
                                        if (seps[k].Trim() != "")
                                        {
                                            fieldType = seps[k].Trim();
                                            fieldTypeIndexInSeps = k;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    fieldType = seps[0].Trim();
                                    fieldTypeIndexInSeps = 0;
                                }

                                string fieldName = "";
                                for (int k = fieldTypeIndexInSeps + 1; k < seps.Length; k++)
                                {
                                    if (seps[k].Trim() != "")
                                    {
                                        fieldName = seps[k].Trim();
                                        if (fieldName.Contains("="))
                                        {
                                            int index = fieldName.IndexOf("=");
                                            fieldName = fieldName.Substring(0, index).Trim();
                                        }
                                        break;
                                    }
                                }

                                if (fieldType == "uint32" ||
                                    fieldType == "uint64" ||
                                    fieldType == "double" ||
                                    fieldType == "float" ||
                                    fieldType == "int32" ||
                                    fieldType == "int64" ||
                                    fieldType == "sint32" ||
                                    fieldType == "sint64" ||
                                    fieldType == "fixed64" ||
                                    fieldType == "sfixed32" ||
                                     fieldType == "sfixed64")
                                {
                                    fieldType = "number";
                                }
                                else if (fieldType == "string")
                                {
                                    fieldType = "string";
                                }
                                else if (fieldType == "bool")
                                {
                                    fieldType = "boolean";
                                }
                                else
                                {
                                    if (ProtoEnumWriter.enumDic.ContainsKey(fieldType))
                                    {
                                        fieldType = "ProtoEnum." + fieldType;
                                    }


                                }
                                if (line.Contains("repeated"))
                                {
                                    fieldType = fieldType + "[]";
                                }
                                string fieldAnotationStr = "---@field public " + fieldName + " " + fieldType+ annotation;
                                allLines.Add(fieldAnotationStr);
                            }

                        }

                    }

                    string messageStr = "local " + entity.entityName + " = {}";
                    allLines.Add(messageStr);
                    allLines.Add("");//换行
                }

            }


            File.WriteAllLines(outputPath, allLines.ToArray());

            Console.WriteLine("write EmmyHint lua success:" + outputPath);
        }
    }
}
