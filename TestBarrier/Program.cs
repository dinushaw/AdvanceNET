using System;
using Dinusha.ConcurrancyUtilities;
using Thread = System.Threading.Thread;

namespace TestBarrier
{
	class MainClass
	{
		static Barrier mybarrier;
		public static void Main (string[] args)
		{	
			int size = 3;
			mybarrier = new Barrier (size);
			Console.WriteLine ("Barrier Size set to :"+size);

			Thread t1 = new Thread (Mymethod);
			t1.Name= "Thread 1";
			t1.Start ();

			Thread t2 = new Thread (Mymethod);
			t2.Name= "Thread 2";
			t2.Start ();

			Thread t3 = new Thread (Mymethod);
			t3.Name= "Thread 3";
			Thread.Sleep (3000);
			t3.Start ();

			Thread t4 = new Thread (Mymethod);
			t4.Name= "Thread 4";
			t4.Start ();




		}

		public static void Mymethod(){
			Console.WriteLine (Thread.CurrentThread.Name + " Waiting to enter barrier ");
			mybarrier.Arrive ();
			Console.WriteLine (Thread.CurrentThread.Name + " Continuing... ");

		}
	}
}
