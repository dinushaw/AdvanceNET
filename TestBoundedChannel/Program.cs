using System;
using Dinusha.ConcurrancyUtilities;
using Thread = System.Threading.Thread;
namespace TestBoundedChannel
{
	class MainClass
	{
		static BoundChannel<int> myBoundedChannel = new BoundChannel<int> (5);

		static int count=10;

		public static void Main (string[] args)
		{
			Thread t1 = new Thread (AddItem);
			t1.Name= "thread 1";


			Thread t2 = new Thread (AddItem);
			t2.Name= "thread 2";


			Thread t3 = new Thread (AddItem);
			t3.Name= "thread 3";


			Thread t4 = new Thread (RemoveItem);
			t4.Name= "Thread 4";


			t1.Start ();
			t4.Start ();	//t2.Start ();
			//t3.Start ();
		}

		public static void AddItem(){
			while (true) {

				myBoundedChannel.Put (count);
				Console.WriteLine (Thread.CurrentThread.Name+" Added "+count);
				count++;

			}
		
		}

		public static void RemoveItem(){
			while (true){
				Thread.Sleep (3000);
				Console.WriteLine ("\t\t\t\t\t waiting to remove");
				int temp = myBoundedChannel.Take();
				Console.WriteLine ("\t\t\t\t\t"+temp+" is removed ");

			}
		
		}
	}
}
