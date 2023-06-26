using LuaProtobufTool.Entity;
using LuaProtobufTool.Reader;
using LuaProtobufTool.Writer;
using ProtobufTool.Writer.CS;
using System;
using System.Diagnostics;

namespace ProtobufTool
{
    internal class Program
    {
        static string binDir = "";
        static string originProtoFileDic = "";

        static string[] protoFilePaths;

        static string outputLuaProtoPath;
        static string outputLuaHintPath;

        static string outputCSProtoPath;


        static List<ProtoEntity> protoEntities = new List<ProtoEntity>();
        static void Main(string[] args)
        {
            Console.WriteLine("begin handle proto files---->");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Main args[{i}]:{args[i]}");
            }

            init(args);


            //read all proto files
            protoFilePaths = Directory.GetFiles(originProtoFileDic, "*.proto", SearchOption.TopDirectoryOnly);
            List<ProtoEntity> allEntityList = new List<ProtoEntity>();
            for (int i = 0; i < protoFilePaths.Length; i++)
            {
                var path = protoFilePaths[i];
                var fileEntities = ProtoReader.Read(path);

                if (fileEntities.Count > 0)
                {
                    allEntityList.AddRange(fileEntities);
                }
            }


            //导出Proto.lua文件       
            BasicProtoWriter.Write(allEntityList, outputLuaProtoPath + @"\Proto.lua");
            //导出ProtoEnum.lua文件 
            ProtoEnumWriter.Write(allEntityList, outputLuaProtoPath + @"\ProtoEnum.lua");
            //导出ProtoMsgID.lua文件
            ProtoMsgIDWriter.Write(allEntityList, outputLuaProtoPath + @"\ProtoMsgID.lua");
            //导出Emmy lua需要的hint文件 
            EmmyHintWriter.Write(allEntityList, outputLuaHintPath + @"\proto.lua");

            //导出CS ProtobufMsgID.cs文件
            ProtoCSMsgIDWriter.Write(allEntityList, outputCSProtoPath + @"\ProtobufMsgID.cs");

            Console.ReadLine();
        }


        static void init(string[] args)
        {
            string curDir = Environment.CurrentDirectory;
            if (curDir.LastIndexOf(@"\Debug") != -1)
            {
                binDir = curDir.Substring(0, curDir.LastIndexOf(@"\Debug"));
            }
            else if (curDir.LastIndexOf(@"\Release") != -1)
            {
                binDir = curDir.Substring(0, curDir.LastIndexOf(@"\Release"));
            }
            else
            {
                Console.WriteLine("error,no Debug or Release folder");
            }

            //not assign params
            if (args.Length == 0)
            {
                Console.WriteLine("curDir:" + curDir + ",binDir:" + binDir);
                originProtoFileDic = binDir + @"\ProtoFiles";

                outputLuaProtoPath = binDir + @"\code_output_lua\Proto";
                outputLuaHintPath = binDir + @"\code_output_lua\EmmyHint";

                outputCSProtoPath = binDir + @"\code_output_cs";
            }
            else
            {
                if (args.Length != 4)
                {
                    throw new Exception("error, args must be 3");
                }
                else
                {
                    originProtoFileDic = Path.Combine(binDir, args[0]);
                    outputLuaProtoPath = Path.Combine(binDir, args[1]);
                    outputLuaHintPath = Path.Combine(binDir, args[2]);

                    outputCSProtoPath = Path.Combine(binDir, args[3]);
                }
            }


            if (!Directory.Exists(originProtoFileDic))
            {
                throw new Exception("no dic:" + originProtoFileDic);
            }
            if (!Directory.Exists(outputLuaProtoPath))
            {
                Directory.CreateDirectory(outputLuaProtoPath);
            }
            if (!Directory.Exists(outputLuaHintPath))
            {
                Directory.CreateDirectory(outputLuaHintPath);
            }

            if (!Directory.Exists(outputCSProtoPath))
            {
                Directory.CreateDirectory(outputCSProtoPath);
            }

            Console.WriteLine("originProtoFileDic:" + originProtoFileDic);
            Console.WriteLine("outputLuaProtoPath:" + outputLuaProtoPath);
            Console.WriteLine("outputLuaHintPath:" + outputLuaHintPath);
            Console.WriteLine("outputCSProtoPath:" + outputCSProtoPath);
        }
    }
}