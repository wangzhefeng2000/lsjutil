﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Lsj.Util.Collections;
using Lsj.Util.Net.Socks5;
using System.Net;
using Lsj.Util.Binary;
using Lsj.Util.JSON;
using System.Text;
using Lsj.Util.Text;

namespace Lsj.Util.Debugger
{
    class Program
    {
        public static void Main()
        {
            //var x = new Socks5Server();
            //x.IP = IPAddress.Loopback;
            //x.Port = 1080;
            //x.Start();

            //var x = new PEFile(@"R:\a.exe");
            //Console.ReadLine();
            //byte[] x = new byte[] { 1, 1, 1, 1, 1 };

            //var s = System.Environment.TickCount;

            //for (int i = 0; i < 100000000; i++)
            //{
            //    var r = 1.ConvertToBytes();
            //}
            //var e = System.Environment.TickCount;

            //Console.WriteLine(e - s);
            //s = System.Environment.TickCount;

            //for (int i = 0; i < 100000000; i++)
            //{
            //    var r = BitConverter.GetBytes(1);
            //}
            //e = System.Environment.TickCount;
            //Console.WriteLine(e - s);
            //Console.ReadLine();


            //var sb = new StringBuilder("aaaaa");
            //sb.RemoveLast(1);
            //Console.WriteLine(sb);



            var x1 = @"""\u0040""";
            //var result1 = JSONParser.Parse(x1);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result1));
            Console.WriteLine(JSONFormater.Format(x1));
            var x2 = @"-0.5";
            //var result2 = JSONParser.Parse(x2);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result2));
            Console.WriteLine(JSONFormater.Format(x2));
            var x3 = @"{""a"": 1,""b"":""x"",""c"":{""a"": 1,""b"":""x""}}";
            //var result31 = JSONParser.Parse<TestClass>(x3);
            //var result32 = JSONParser.Parse(x3);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result31));
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result32));
            Console.WriteLine(JSONFormater.Format(x3));
            var x4 = @"[1,2,3]";
            //var result41 = JSONParser.Parse(x4);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result41));
            //var result42 = JSONParser.Parse<List<int>>(x4);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result42));
            Console.WriteLine(JSONFormater.Format(x4));
            var x5 = @"[1,2,3,{""a"": 1,""b"":""x"",""c"":{""a"": 1,""b"":""x""}}]";
            //var result5 = JSONParser.Parse(x5);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result5));
            Console.WriteLine(JSONFormater.Format(x5));
            var x6 = @"[{""a"": 1,""b"":""x"",""c"":{""a"": 1,""b"":""x""}},{""a"": 1,""b"":""x"",""c"":{""a"": 1,""b"":""x""}}]";
            //var result6 = JSONParser.Parse<List<TestClass>>(x6);
            //Console.WriteLine(JSONConverter.ConvertToJSONString(result6));
            //Console.ReadLine();
            Console.WriteLine(JSONFormater.Format(x6));
            Console.ReadLine();
        }

    }
    public class TestClass
    {
        public int a
        {
            get;
            set;
        }
        public string b
        {
            get;
            set;
        }
        public TestClass2 c
        {
            get;
            set;
        }
    }
    public class TestClass2
    {
        public int a
        {
            get;
            set;
        }
        public string b
        {
            get;
            set;
        }
    }

}

