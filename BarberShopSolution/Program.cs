using System;
using Semaphore = Dinusha.ConcurrancyUtilities.Semaphore;
using Thread = System.Threading.Thread;


namespace BarberShopSolution
{
	class MainClass
	{
		static int customerCount = 0;
		static int numOfChairs = 3;
		static Semaphore mutex = new Semaphore (1);
		static Semaphore customer = new Semaphore (0);
		static Semaphore barber = new Semaphore(0);

		public static void Main (string[] args)
		{
			Thread t1 = new Thread (Customer);
			t1.Name = "Customer 1";
			t1.Start ();

			Thread t2 = new Thread (Customer);
			t2.Name = "Customer 2";
			t2.Start ();

			Thread t3 = new Thread (Customer);
			t3.Name = "Customer 3";
			t3.Start ();

			Thread t4 = new Thread (Customer);
			t4.Name = "Customer 4";
			t4.Start ();

			Thread b1 = new Thread (Barber);
			b1.Name = "Barber";
			b1.Start ();

		}

		public static void Customer(){
		
			mutex.Acquire ();
			if (customerCount == numOfChairs) {
			
				mutex.Release ();
				Console.WriteLine ("No empty chairs in shop "+Thread.CurrentThread.Name+" Leaves shop");
				return;

			}

			Console.WriteLine (Thread.CurrentThread.Name +" enters shop");
			customerCount++;
			mutex.Release ();

			//custoemr tries to get haircut 
			customer.Release ();
			barber.Acquire ();
			Console.WriteLine (Thread.CurrentThread.Name + " gets haircut ");
			Thread.Sleep (3000);

			//Customer leaves shop after haircut

			mutex.Acquire ();
			customerCount--;
			mutex.Release ();
			barber.Release ();
			Console.WriteLine (Thread.CurrentThread.Name + " Leaves. Finish having haircut");

		
		}

		public static void Barber(){
			customer.Acquire ();
			barber.Release ();
			CutHair ();
		
		
		}

		static void CutHair ()
		{
			Thread.Sleep (2000);
		}
	}
}
