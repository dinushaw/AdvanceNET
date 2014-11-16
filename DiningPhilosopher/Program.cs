using System;
using Thread= System.Threading.Thread;
using Dinusha.ConcurrancyUtilities;

namespace DiningPhilosopher
{
	class MainClass
	{

		static int philID=0;
		static Semaphore[] forks = new Semaphore[]{new Semaphore(1),new Semaphore(1),new Semaphore(1),new Semaphore(1),new Semaphore(1)};
		static Semaphore footman = new Semaphore(4);
		static object lockobject = new object ();


		public static void Main (string[] args)
		{
			Thread t1 = new Thread (GetForks);
			t1.Name = "Phil 0";
			t1.Start ();


			Thread t2 = new Thread (GetForks);
			t2.Name = "Phil 1";
			Thread.Sleep (1000);
			t2.Start ();

			Thread t3 = new Thread (GetForks);
			t3.Name = "Phil 2";
			Thread.Sleep (1000);
			t3.Start ();

			Thread t4 = new Thread (GetForks);
			t4.Name = "Phil 3";
			Thread.Sleep (1000);
			t4.Start ();

			Thread t5 = new Thread (GetForks);
			t5.Name = "Phil 4";
			Thread.Sleep (1000);
			t5.Start ();
		}

		public static void GetForks(){


				footman.Acquire ();
				if (philID <= 4) {

					philID++;


				}


				if ((philID - 1) % 2 == 0) {

					Console.WriteLine ("\t\t" + Thread.CurrentThread.Name + " Waiting for Right Fork ");
					forks [Right_fork (philID)].Acquire ();
					Console.WriteLine (Thread.CurrentThread.Name + " Got right Fork ");
					Console.WriteLine ("\t\t" + Thread.CurrentThread.Name + " Waiting for Left Fork ");
					forks [Left_fork (philID)].Acquire ();
					Console.WriteLine (Thread.CurrentThread.Name + " Got Left Fork ");


					Console.WriteLine ("\t\t\t" + Thread.CurrentThread.Name + " is Eating... ");


				} else if ((philID - 1) % 2 == 1) {


					Console.WriteLine ("\t\t" + Thread.CurrentThread.Name + " Waiting for Left Fork  ");
					forks [Left_fork (philID)].Acquire ();
					Console.WriteLine (Thread.CurrentThread.Name + " Got Left Fork ");
					Console.WriteLine ("\t\t" + Thread.CurrentThread.Name + " Waiting for Right Fork ");
					forks [Right_fork (philID)].Acquire ();
					Console.WriteLine (Thread.CurrentThread.Name + " Got Right Fork ");

					Console.WriteLine ("\t\t\t" + Thread.CurrentThread.Name + " is Eating... ");


				}

				Thread.Sleep (3000);
				PutForks (philID - 1);


				
		}

		public static void PutForks(int pid ){
		
			Console.WriteLine ("\t\t\t" + Thread.CurrentThread.Name + " has Finish  Eating... ");
			lock (lockobject) {
				forks [Left_fork (pid)].Release ();
				forks [Right_fork (pid)].Release ();
			}
			footman.Release ();

			
		
		}

		public static int Left_fork(int philid){
			return philid;
		}

		public static int Right_fork(int philid){
			return (philid + 1) % 5;
		}
	}
}
