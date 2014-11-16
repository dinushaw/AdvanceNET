using System;
using Semaphore = Dinusha.ConcurrancyUtilities.Semaphore;
using LightSwitch = Dinusha.ConcurrancyUtilities.LightSwitch;
using Thread = System.Threading.Thread;


namespace UniSexBathRoomProblem
{
	class MainClass
	{
		static Semaphore queueTs = new Semaphore(1);
		static Semaphore keySem = new Semaphore(1);
		static LightSwitch femailLS = new LightSwitch(keySem);

		static Semaphore popCounter = new Semaphore(3);
		static LightSwitch mailLS = new LightSwitch(keySem);


		public static void Main (string[] args)
		{

			Thread f1 = new Thread (Female);
			f1.Name = "female 1";
			f1.Start ();

			Thread f2 = new Thread (Female);
			f2.Name = "female 2";
			f2.Start ();

			Thread f3 = new Thread (Female);
			f3.Name = "female 3";
			f3.Start ();

			Thread m1 = new Thread (Male);
			m1.Name = "Male 1";
			m1.Start ();

			Thread m2 = new Thread (Male);
			m2.Name = "Male 2";
			m2.Start ();

			Thread m3 = new Thread (Male);
			m3.Name = "Male 3";
			m3.Start ();


		}

		public static void Female(){
			queueTs.Acquire ();
			femailLS.Acquire ();

			//Console.WriteLine ("Females can enter bathroom");
			queueTs.Release ();
			popCounter.Acquire ();

			Console.WriteLine (Thread.CurrentThread.Name + " occupies the bathroom");
			Thread.Sleep (2000);


			popCounter.Release ();
			Console.WriteLine (Thread.CurrentThread.Name + " leaves bathroom");
			femailLS.Release ();

		
		}

		public static void Male(){

			queueTs.Acquire ();
			mailLS.Acquire ();
			//Console.WriteLine ("\t\t\t Males can enter the bathroom ");
			queueTs.Release ();
			popCounter.Acquire ();

			Console.WriteLine ("\t\t\t "+Thread.CurrentThread.Name + " occupies the bathroom");
			Thread.Sleep(2000);


			popCounter.Release();
			Console.WriteLine ("\t\t\t"+Thread.CurrentThread.Name + " leaves  bathroom");
			mailLS.Release();




		}
	}
}
