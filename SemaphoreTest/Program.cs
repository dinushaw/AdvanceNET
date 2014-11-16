using System;
using Dinusha.ConcurrancyUtilities;
using Thread= System.Threading.Thread;


namespace SemaphoreTest
{
	class MainClass
	{
		static Semaphore mySem = new Semaphore (2);
		static Random rnd = new Random ();
		static int rannum;

		public static void Main (string[] args)
		{
			Thread t1 = new Thread (DoAquire);
			t1.Name="Thread 1";
			Thread t2 = new Thread (DoRelease);
			t2.Name="Thread 2";
			t1.Start ();
			t2.Start ();
			Thread t3 = new Thread (DoAquire);
			t3.Name="Thread 3";
			t3.Start ();

		}

		public static void DoAquire(){

			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name+" Waiting to Acquire Token");
				mySem.Acquire ();
				Console.WriteLine (Thread.CurrentThread.Name+" Aquired Token ");

			}
		
		}
		public static void DoRelease(){


			while (true) {
				//Console.WriteLine ("\t\tWaiting to Release");
				Thread.Sleep (1000);
				rannum = rnd.Next (1, 3);
				mySem.Release (rannum);
				Console.WriteLine ("\t\t"+Thread.CurrentThread.Name+" has Released "+rannum+" Token(s)" );
			}


		
		}
	}
}
