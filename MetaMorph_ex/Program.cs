using dnlib.DotNet;
using System;
using System.Diagnostics;
using System.IO;

namespace MetaMorph_ex
{
    class Program
    {
        /// <summary>
        /// the flag which will be changed
        /// </summary>
        public static string flag = "1337";
        /// <summary>
        /// <see cref="ModuleDef"/>
        /// a .NET file representation
        /// </summary>
        public static ModuleDefMD file_module;
        /// <summary>
        /// Pure Byte array of current file
        /// </summary>
        public static byte[] file_bytes;
        /// <summary>
        /// Absolute path of current file
        /// </summary>
        public static string me;
        /// <summary>
        /// A new instance of Random
        /// </summary>
        public static Random rnd;
        /// <summary>
        /// Program Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //because we may want to avoid zombie process 0:)
            if (!File.Exists("go.txt")) Environment.Exit(0);
            //First we read this file bytes
            readcode();
            //Second we edit Flag value
            replaceflag();
            //Finally we save new file
            writebyte();
            //Just an excuse to use changed code at runtime ;)
            Console.WriteLine(flag);
        }
        /// <summary>
        /// Method to grab current file bytes / module
        /// </summary>
        static void readcode()
        {
            //get current file absolute path
            me = (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + AppDomain.CurrentDomain.FriendlyName).Replace(".vshost", "").Replace("file:\\", "");
            //get bytes from absolute path
            file_bytes = File.ReadAllBytes(me);
            //create a new moduledef using these bytes
            file_module = ModuleDefMD.Load(file_bytes);
        }
        /// <summary>
        /// Method to change Flag value
        /// </summary>
        static void replaceflag()
        {
            //get MetaMorph_ex.Program method constructor
            MethodDef method = file_module.ResolveMethod(6);
            //declare a new instance of random
            rnd = new Random();
            //change flag value in module
            method.Body.Instructions[0].Operand = Helper.GenerateString(rnd.Next(0, 20));
        }

        /// <summary>
        /// Method to save module
        /// </summary>
        static void writebyte()
        {
            //declare a new instance of random
            rnd = new Random();
            //get new file name
            me = Helper.RandomAZString(rnd.Next(0, 20)) + ".exe";
            //save module file 
            file_module.Write(me);
            //start created self modifying assembly
            Process.Start(me);
            //die
            Environment.Exit(0);
        }
    }
}