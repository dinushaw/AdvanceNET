using System;
using Thread = System.Threading.Thread;
using Semaphore = Dinusha.ConcurrancyUtilities.Semaphore;
using Mutex = Dinusha.ConcurrancyUtilities.Mutex;


namespace SmokersProblem
{
	class MainClass
	{
		static Semaphore AgentSem; 
		static Semaphore Tobbaco;
		static Semaphore Paper;
		static Semaphore Matches; 

		static Semaphore TobbacoSem;
		static Semaphore PaperSem;
		static Semaphore MatchesSem; 


		static  Semaphore myMutex; 

		static bool isTobbaco, isPaper, isMatches = false;


		public static void Main (string[] args)
		{
			AgentSem = new Semaphore (1);
			Tobbaco = new Semaphore (0);
			Paper = new Semaphore (0);
			Matches = new Semaphore (0);


			TobbacoSem = new Semaphore (0);
			PaperSem = new Semaphore (0);
			MatchesSem = new Semaphore (0);

			myMutex = new Semaphore (1);



		

			/// Pushers 
			Thread PusherTTh = new Thread (TobbacoPusher);
			PusherTTh.Name = "Pusher with Tobbaco";
			PusherTTh.Start ();

			Thread PusherMTh = new Thread (MatchPusher);
			PusherMTh.Name = "Pusher with Matches";
			PusherMTh.Start ();

			Thread PusherPTh = new Thread (PaperPusher);
			PusherPTh.Name = "Pusher with Paper";
			PusherPTh.Start ();


			///Smokers 

			Thread SmokerTTh = new Thread (SmokerT);
			SmokerTTh.Name = "Smoker with Tobbaco";
			SmokerTTh.Start ();

			Thread SmokerMTh = new Thread (SmokerM);
			SmokerMTh.Name = "Smoker with Matches";
			SmokerMTh.Start ();

			Thread SmokerPTh = new Thread (SmokerP);
			SmokerPTh.Name = "Smoker with Paper";
			SmokerPTh.Start ();


			Thread AgentAt = new Thread (AgentA);
			AgentAt.Name = "Agent A";
			AgentAt.Start ();

			Thread AgentBt = new Thread (AgentB);
			AgentBt.Name = "Agent B";
			AgentBt.Start ();

			Thread AgentCt = new Thread (AgentC);
			AgentCt.Name = "Agent C";
			AgentCt.Start ();
		}


		public static void ChooseAgent(){
		
			Random rnd = new Random ();

			while (true) {

				switch (rnd.Next (1, 3)) {

				case 1:
					AgentA ();
					break;
				case 2:
					AgentB ();
					break;
				case 3:
					AgentC ();
					break;

				}

			}
			
		}

		public static void TobbacoPusher(){

			while (true) {

				//	Console.WriteLine(Thread.CurrentThread.Name +"Waiting to get Tobbaco");
				Tobbaco.Acquire ();
			myMutex.Acquire ();

			if (isPaper) {
				isPaper = false;
				MatchesSem.Release ();
			} else if (isMatches) {
				isMatches = false;
				PaperSem.Release ();
				
			} else {
				isTobbaco = true;

			}

			myMutex.Release ();
			}
		}

		public static void PaperPusher(){
			while (true) {

				//	Console.WriteLine(Thread.CurrentThread.Name +"Waiting to get Paper");
				Paper.Acquire ();
				myMutex.Acquire ();

				if (isMatches) {
					isMatches = false;
					TobbacoSem.Release ();
				} else if (isTobbaco) {
					isTobbaco = false;
					MatchesSem.Release ();

				} else {
					isPaper = true;

				}

				myMutex.Release ();
			}

		
		}

		public static void MatchPusher(){

			while (true) {
				//Console.WriteLine(Thread.CurrentThread.Name +"Waiting to get Matches");

				Matches.Acquire ();
				myMutex.Acquire ();

				if (isPaper) {
					isPaper = false;
					TobbacoSem.Release ();
				} else if (isTobbaco) {
					isTobbaco = false;
					PaperSem.Release ();

				} else {
					isMatches = true;

				}
				myMutex.Release ();

			}
		}


		public static void AgentA(){

			while (true) {
				//Console.WriteLine ("Agent A has Tobbaco and Paper");
				AgentSem.Acquire ();
				Console.WriteLine ("Agent A release Tobbaco and Paper");
				Tobbaco.Release ();
				Paper.Release ();

			}

		}

		public static void AgentB(){
			while (true) {
				//Console.WriteLine ("Agent B has Paper and Matches");
				AgentSem.Acquire ();
				Console.WriteLine ("Agent B release Matches and Paper");
				Paper.Release ();
				Matches.Release ();
			}


		}

		public static void AgentC(){
			while (true) {

				//Console.WriteLine ("Agent C has Tobbaco and Paper");
				AgentSem.Acquire ();
				Console.WriteLine ("Agent C release Tobbaco and Matches");
				Tobbaco.Release ();
				Matches.Release ();
			}

		}

		/// <summary>
		/// Smoker Methods  
		/// </summary>

		public static void SmokerM(){
			while (true) {
				Console.WriteLine ("Smoker(M) waiting for T P");
				MatchesSem.Acquire ();
				AgentSem.Release ();
				Smoke ();

			}

		
		}

		public static void SmokerT(){
			while (true) {
				Console.WriteLine ("Smoker(T) waiting for M P");
				TobbacoSem.Acquire ();
				Smoke ();
				AgentSem.Release ();

			}


		}

		public static void SmokerP(){
			while (true) {
				Console.WriteLine ("Smoker(P) waiting for T M");
				PaperSem.Acquire ();
				Smoke ();
				AgentSem.Release ();
			}


		}


		static void Smoke ()
		{
			Console.WriteLine (Thread.CurrentThread.Name + " is Smoking...");
			Thread.Sleep (2000);
		}
	}
}
