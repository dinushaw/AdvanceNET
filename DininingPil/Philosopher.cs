using System;
using Thread = System.Threading.Thread;
using System.Threading;
using Dinusha.ConcurrancyUtilities;
using Semaphore = Dinusha.ConcurrancyUtilities.Semaphore;

namespace DininingPil
{
	public class Philosopher
	{
		public int philID=0;
		public Semaphore[] Forks;

		public Philosopher (int id, Semaphore[] forks )
		{
			philID = id;
			Forks = forks;

			Thread t1 = new Thread (philActivity);
			t1.Name = "Phil " + philID;
			t1.Start();

		}

		public void philActivity(){
			while (true) {
				//	Think ();
				GetForks (Forks[Right_fork(philID)],Forks[Left_fork(philID)],philID);
				//Eat ();
				Thread.Sleep (1000);
				PutForks (Forks [Right_fork (philID)], Forks [Left_fork (philID)], philID);
			}

		}

		public void Eat(){

			Console.WriteLine ("Philosopher"+philID+" is Eating... ");
			Thread.Sleep (3000);
			Console.WriteLine ("Philosopher"+philID+" has finish  Eating ");
		}

		public void Think(){

			Console.WriteLine ("Philosopher"+philID+" is Thinking... ");
			Thread.Sleep (3000);
			Console.WriteLine ("Philosopher"+philID+" has finish  Thinking... ");
		}

		public void GetForks(Semaphore Right, Semaphore Left, int philid ){
		
			//lock (this) {	
			
			if(philid %2 ==0){

					Console.WriteLine ("\t\t"+philID +" Waiting for Right Fork ");
				Right.Acquire ();
					Console.WriteLine (philID +" Got right Fork ");
					Console.WriteLine ("\t\t"+philID +" Waiting for Left Fork ");
				Left.Acquire ();
					Console.WriteLine (philID +" Got Left Fork ");

					Console.WriteLine ("\t\t\t"+philID +" is Eating... ");
			}


			else if(philid % 2 == 1){


					Console.WriteLine ("\t\t"+philID +" Waiting for Left Fork  ");
				Left.Acquire ();
					Console.WriteLine (philID +" Got Left Fork ");
					Console.WriteLine ("\t\t"+philID +" Waiting for Right Fork ");
				Right.Acquire ();
					Console.WriteLine (philID +" Got Right Fork ");

					Console.WriteLine ("\t\t\t"+philID +" is Eating... ");

			}
			//	}
		}

		public void PutForks(Semaphore Right, Semaphore Left, int philid){

			Console.WriteLine ("\t\t\t"+philID +" has finish Eating... ");
			Right.Release ();
			Left.Release ();

		
		}



		public static int Left_fork(int philid){
			return philid;
		}

		public static int Right_fork(int philid){
			return (philid + 1) % 5;
		}

	}
}

