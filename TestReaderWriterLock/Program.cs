using System;
using Thread= System.Threading.Thread;
using Dinusha.ConcurrancyUtilities;

namespace TestReaderWriterLock
{
	class MainClass
	{
		static ReaderWriterLock myrwLock;

		public static void Main (string[] args)
		{
			myrwLock = new ReaderWriterLock ();

			Thread t1 = new Thread (Reading);
			t1.Name= "Thread 1";
			t1.Start ();

			Thread t2 = new Thread (Reading);
			t2.Name= "Thread 2";
			t2.Start ();

			Thread t3 = new Thread (Writing);
			t3.Name= "Thread 3";
			t3.Start ();


		}

		public static void Reading(){

			for (int i = 0; i < 4; i++) {

				Console.WriteLine (Thread.CurrentThread.Name +" Waiting to Read.");
				myrwLock.ReadersAcquire ();
				Console.WriteLine (Thread.CurrentThread.Name +" Reading...");

				myrwLock.ReadersRelease ();
				Console.WriteLine(Thread.CurrentThread.Name +" Finish Reading.");
			}


		}

		public static void Writing(){
		
			for (int i = 0; i < 3; i++) {
				Console.WriteLine ("\t\t\t"+Thread.CurrentThread.Name +" Waiting to Write.");
				myrwLock.WritersAcquire ();
				Console.WriteLine ("\t\t\t"+Thread.CurrentThread.Name +" Writing...");

				myrwLock.WritersRelease ();
				Console.WriteLine("\t\t\t"+Thread.CurrentThread.Name +" Finish Writing.");
			}
		
		}
	}
}
