using System;
using Thread = System.Threading.Thread;
using Dinusha.ConcurrancyUtilities;

namespace ChannelTest
{
	class MainClass
	{
		static Channel<int> mychannel = new Channel<int>();
		static Random rnd = new Random ();
		static int myNumber;


		public static void Main (string[] args)
		{
			Thread t4 = new Thread (() => RemoveNumber());
			t4.Name = "Thread 4 ";
			t4.Start ();

			Thread t5 = new Thread (RemoveNumber);
			t5.Name = "Thread 5 ";
			t5.Start ();

			Thread.Sleep (2000);

			Thread t = new Thread (RemoveNumber);
			t.Name = "Thread 1 ";
			t.Start ();


			Thread t2 = new Thread (() => AddNumber() );
			t2.Name = "Thread 2 ";
			t2.Start ();



			Thread t3 = new Thread (() => AddNumber() );
			t3.Name = "Thread 3 ";
			t3.Start ();
		}


		public static void AddNumber(){
			while (true) {
				//Console.WriteLine ("\t\t\t"+Thread.CurrentThread.Name+" about to put "+myNumber  );
				myNumber = rnd.Next (1, 10);
				mychannel.Put (myNumber);
				Console.WriteLine ("\t\t\t"+myNumber + " Added by "+Thread.CurrentThread.Name);
				Thread.Sleep (5000);}

		}

		public static void RemoveNumber(){
			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name+ " Waiting to Take ");
				int temp = (int)mychannel.Take ();
				Thread.Sleep (2000);
				Console.WriteLine (temp +" Taken by "+Thread.CurrentThread.Name);
			}
		
		}
	}
}
