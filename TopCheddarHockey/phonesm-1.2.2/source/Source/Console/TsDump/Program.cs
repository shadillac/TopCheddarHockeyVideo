// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Henric Jungheim">
//  Copyright (c) 2012-2014.
//  <author>Henric Jungheim</author>
//  </copyright>
// -----------------------------------------------------------------------
// Copyright (c) 2012-2014 Henric Jungheim <software@henric.org>
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics;
using SM.Media.Utility;
using SM.TsParser;

namespace TsDump
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            try
            {
                foreach (var arg in args)
                {
                    Console.WriteLine("Reading {0}", arg);

                    using (var mediadump = new MediaDump(ProgramStreamsHandler))
                    {
                        mediadump.ReadAsync(arg).Wait();

                        mediadump.CloseAsync().Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (Debugger.IsAttached)
                    Debugger.Break();
            }

            try
            {
                TaskCollector.Default.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (Debugger.IsAttached)
                    Debugger.Break();
            }
        }

        static void ProgramStreamsHandler(IProgramStreams programStreams)
        {
            Console.WriteLine("Program: " + programStreams.ProgramNumber);

            foreach (var s in programStreams.Streams)
                Console.WriteLine("   {0}({1}): {2}", s.StreamType.Contents, s.Pid, s.StreamType.Description);
        }
    }
}
