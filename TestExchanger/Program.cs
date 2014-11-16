using System;
using Dinusha.ConcurrancyUtilities;
using Thread = System.Threading.Thread;


namespace TestExchanger
{
	class MainClass
	{
		static Exchanger<String> myExchanger;

		public static void Main (string[] args)
		{
			myExchanger = new Exchanger<String> ();

			Thread t1 = new Thread (mymethod);
			t1.Name ="Thread 1 ";
			t1.Start ();

			Thread t2 = new Thread (myNextMethod);
			t2.Name ="Thread 2 ";
			Thread.Sleep (1000);
			t2.Start ();

			Thread t3 = new Thread (mymethod);
			t3.Name ="Thread 3 ";
			t3.Start ();

			Thread t4 = new Thread (myNextMethod);
			t4.Name ="Thread 4 ";
			Thread.Sleep (1000);
			t4.Start ();

		}

		public static void mymethod(){
		
			String send = "MyString01" + Thread.CurrentThread.Name;
			String temp = (String)myExchanger.Exchange (send);
			Console.WriteLine ("Object needed to Exchange by "+Thread.CurrentThread.Name+" : " + send);
			Console.WriteLine ("Object Recieved by "+Thread.CurrentThread.Name+" : " + temp);
		
			
		}

		public static void myNextMethod(){

			String send2 = "Something Else"+Thread.CurrentThread.Name;
			String temp2 = (String)myExchanger.Exchange (send2);
			Console.WriteLine ("Object needed to Exchange by "+Thread.CurrentThread.Name+" : " + send2);
			Console.WriteLine ("Object Recieved by "+ Thread.CurrentThread.Name+" : " +temp2);
		}
	}
}
