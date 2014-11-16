using System;
using Dinusha.ConcurrancyUtilities;
using Thread= System.Threading.Thread;
using System.Collections.Generic;

namespace TestFIFOSemaphore
{
	class MainClass
	{
		static FIFOSemaphore myFifo;
		public static void Main (string[] args)
		{
			myFifo = new FIFOSemaphore(0);

		
			Thread t1 = new Thread (Mymethod);
			t1.Name= "Thread 1";
			t1.Start ();

			Thread t2 = new Thread (Mymethod);
			t2.Name= "Thread 2";
			//Thread.Sleep (1000);
			t2.Start ();

			Thread t3 = new Thread (Mymethod);
			t3.Name= "Thread 3";
			//Thread.Sleep (2000);
			t3.Start ();

			Thread t5 = new Thread (Mymethod);
			t5.Name= "Thread 5";
			t5.Start ();

			Thread t6 = new Thread (Mymethod);
			t6.Name= "Thread 6";
			//Thread.Sleep (1000);
			t6.Start ();

			Thread t7 = new Thread (Mymethod);
			t7.Name= "Thread 7";
			//Thread.Sleep (2000);
			t7.Start ();

			Thread.Sleep (3000);
			Thread t4 = new Thread (myReleseMethod);
			t4.Name= "Thread 4";
			t4.Start ();
		






		}

		public static void Mymethod(){
			Console.WriteLine ("Waiting to  Aquired by "+Thread.CurrentThread.Name);

			myFifo.Acquire ();

			Console.WriteLine ("Aquired by "+Thread.CurrentThread.Name);

		}

		public static void myReleseMethod(){

			for (int i = 0; i < 3; i++) {
				Thread.Sleep (3000);
				myFifo.Release ();
				Console.WriteLine (" one token Released");
			}





		}
	}
}
