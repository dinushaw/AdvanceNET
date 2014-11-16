using System;
using Dinusha.ConcurrancyUtilities;
using Thread= System.Threading.Thread;

namespace TestMutex
{
	class MainClass
	{
		static Mutex myMutex = new Mutex();
		static Random rnd = new Random ();
		static int rannum;

		public static void Main (string[] args)
		{
			Thread t1 = new Thread (DoAquire);
			t1.Name="Thread 1";
			t1.Start ();

			Thread t2 = new Thread (DoRelease);
			t2.Name="Thread 2";
			t2.Start ();

			Thread t3 = new Thread (DoAquire);
			t3.Name="Thread 3";
			t3.Start ();

		}

		public static void DoAquire(){

			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name+" Waiting to Acquire Token");
				myMutex.Acquire ();
				Console.WriteLine (Thread.CurrentThread.Name+" Aquired Token ");

			}

		}
		public static void DoRelease(){


			while (true) {
				//Console.WriteLine ("\t\tWaiting to Release");
				Thread.Sleep (1000);
				//rannum = rnd.Next (1, 3);
				myMutex.Release ();
				//Console.WriteLine ("\t\t"+Thread.CurrentThread.Name+" has Released "+rannum+" Token(s)" );
				Console.WriteLine ("\t\t" + Thread.CurrentThread.Name);
			}



		}
	}
}
