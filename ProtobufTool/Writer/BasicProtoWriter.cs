using LuaProtobufTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaProtobufTool.Writer
{
    public class BasicProtoWriter
    {
        public static void Write(List<ProtoEntity> allEntities, string outputPath)
        {
            int lineCount = 0;
            for (int i = 0; i < allEntities.Count; i++)
            {
                var entity = allEntities[i];
                lineCount += entity.allLines.Count;
            }

            lineCount += 4;//前面
            lineCount += 3;//后面


            string[] alllines = new string[lineCount];
            alllines[0] = "---@class Proto";
            alllines[1] = "local Proto={";
            alllines[2] = "Schema=[[";
            alllines[3] = "syntax=\"proto3\"";



            int index = 4;
            for (int i = 0; i < allEntities.Count; i++)
            {
                for (int j = 0; j < allEntities[i].allLines.Count; j++)
                {
                    alllines[index] = allEntities[i].allLines[j];
                    index++;
                }
            }

            alllines[lineCount - 3] = "]]";
            alllines[lineCount - 2] = "}";
            alllines[lineCount - 1] = "return Proto";
            File.WriteAllLines(outputPath, alllines);
            Console.WriteLine("write Proto lua success:" + outputPath);
        }
    }
}
