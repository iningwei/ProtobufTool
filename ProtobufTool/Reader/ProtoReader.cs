using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Reader
{
    //消息体结构
    /*
    // 修改角色基本信息返回
    // S2C_MsgID=100@10
    message S2CMsgChangeRoleBaseResp
    {
        enumeration.PbRoleRetResp result  = 1; // 结果
    }

    // 角色装扮请求信息
    // C2S_MsgID=100@11
    message C2SUserDressUpRep
    {
        repeated uint32 oldDressed = 1; // 之前的装扮信息 [ 备注 可以不发 ]
        repeated uint32 newDressed = 2; // 最新的装扮信息 [ 备注 必填 ]
    }
    */

    //注意消息标号标识使用S2C_MsgID和C2S_MsgID用来标示上行还是下行消息


    public class ProtoReader
    {
        public static List<ProtoEntity> Read(string path)
        {
            List<ProtoEntity> result = new List<ProtoEntity>();


            string[] lines = File.ReadAllLines(path);
            int count = lines.Length;
            if (count > 0)
            {
                int anotationIndex = -1;//没有就是-1
                int entityStartIndex;
                int entityEndIndex;
                bool isEnum = false;
                bool isC2SMsg = false;
                string msgId = "";
                string msgAnotation = "";//消息注释
                string entityName = "";
                for (int i = 0; i < count; i++)
                {
                    var line = lines[i];
                    line = line.Trim();
                    anotationIndex = -1;
                    isEnum = false;
                    isC2SMsg = false;
                    msgId = "";
                    msgAnotation = "";

                    //规避外部嵌套类命名为enums的情况
                    if (line != "" && (line.StartsWith("message") || (line.StartsWith("enum") && line.StartsWith("enumeration") == false)))
                    {
                        if (line.StartsWith("enum"))
                        {
                            isEnum = true;
                            Console.WriteLine("----------> enum:::" + line);

                        }


                        //取entityName
                        var seps = line.Split(' ');
                        for (int k = 1; k < seps.Length; k++)
                        {
                            if (seps[k].Trim() != "")
                            {
                                entityName = seps[k].Trim();
                                if (entityName.EndsWith("{"))
                                {
                                    entityName = entityName.Trim('{');
                                }
                                break;
                            }
                        }



                        entityStartIndex = i;
                        //向上找到注释
                        for (int j = entityStartIndex - 1; j >= 0; j--)
                        {
                            var anotationLine = lines[j];
                            if (anotationLine.StartsWith("//") && anotationLine.Contains("_MsgID") == false)
                            {
                                anotationIndex = j;
                            }
                            else
                            {
                                if (anotationLine.Contains("_MsgID"))
                                {
                                    if (anotationLine.Contains("C2S_"))
                                    {
                                        isC2SMsg = true;
                                    }
                                    msgId = anotationLine.Split('=')[1];
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        //向下找到结束点
                        entityEndIndex = count - 1;
                        for (int k = entityStartIndex + 1; k < count; k++)
                        {
                            var endLine = lines[k];
                            if (endLine.StartsWith("//") ||
                                endLine.StartsWith("message") || (
                                endLine.StartsWith("enum") && endLine.StartsWith("enumeration") == false))
                            {
                                entityEndIndex = k - 1;
                                break;
                            }
                        }

                        //添加entity
                        ProtoEntity entity = new ProtoEntity();
                        entity.isEnum = isEnum;
                        entity.isC2SMsg = isC2SMsg;
                        entity.msgId = msgId;
                        entity.msgAnotation = (anotationIndex != -1 ? lines[anotationIndex] : "");
                        entity.entityName = entityName;
                        for (int p = (anotationIndex != -1 ? anotationIndex : entityStartIndex); p <= entityEndIndex; p++)
                        {
                            var content = lines[p].Trim();
                            //去除content中的嵌套类结构
                            //需要考虑没有注释的情况
                            //需要考虑诸如  嵌套类名.消息体 这种结构
                            //如果.出现在注释符 // 后，则不需要处理(说明是在注释里面出现的.符号)
                            int index = content.IndexOf(".");
                            int annoSlashIndex = content.IndexOf("/");
                            if ((index != -1 && (annoSlashIndex != -1 && index < annoSlashIndex)) ||
                                (index != -1 && annoSlashIndex == -1))
                            {
                                var firstSepIndex = content.IndexOf(' ');
                                if (firstSepIndex != -1 && firstSepIndex < index)
                                {
                                    content = content.Remove(firstSepIndex + 1, index - firstSepIndex);
                                }
                                else
                                {
                                    content = content.Substring(index + 1);
                                }
                            }
                            entity.AddLine(content);
                        }

                        result.Add(entity);
                    }
                }


            }


            Console.WriteLine(path + "'s entity count:" + result.Count);
            return result;
        }
    }
}
