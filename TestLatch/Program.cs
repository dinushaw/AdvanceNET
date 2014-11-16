using System;
using Dinusha.ConcurrancyUtilities;
using Thread = System.Threading.Thread;

namespace TestLatch
{
	class MainClass
	{
		static Latch mylatch = new Latch ();

		public static void Main(String []Args){

			Thread t1 = new Thread (Mymethod);
			t1.Name= "Thread 1";
			t1.Start ();

			Thread t2 = new Thread (Mymethod);
			t2.Name= "Thread 2";
			t2.Start ();

			Thread t3 = new Thread (Mymethod);
			t3.Name= "Thread 3";
			t3.Start ();

			Thread t4 = new Thread (Release);
			t4.Name= "Thread 4";
			t4.Start ();


		}

		public static void Mymethod(){
			Console.WriteLine (Thread.CurrentThread.Name + "acq ");
			mylatch.Acquire ();
			Console.WriteLine (Thread.CurrentThread.Name + "Continuing... ");
		
		}

		public static void Release(){

			Thread.Sleep (5000);
			mylatch.Release ();
			Console.WriteLine (Thread.CurrentThread.Name + "Relese all");
		}



	}
}
