using System;
using Thread=System.Threading.Thread;
using Dinusha.ConcurrancyUtilities;

namespace TestLightSwitch
{
	class MainClass
	{

		static Semaphore mySemaphore = new Semaphore (1);


		static LightSwitch mylightSwitch = new LightSwitch (mySemaphore);




		public static void Main (string[] args)
		{
			Thread t1 = new Thread (Mymethod);
			t1.Name= "Thread 1";
			t1.Start ();

			Thread t2 = new Thread (Mymethod2);
			t2.Name= "Thread 2";
			t2.Start ();

			Thread t3 = new Thread (Release);
			t3.Name= "Thread 3";
			t3.Start ();




		}

		public static void Mymethod(){
			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name + " Acquire Semaphore ");
				mySemaphore.Acquire ();
				Thread.Sleep (3000);
				Console.WriteLine (Thread.CurrentThread.Name + " Relese Semaphore ");
				mySemaphore.Release ();
			}


		}

		public static void Mymethod2(){
			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name + " Waiting to Turn ON Light Switch");
				mylightSwitch.Acquire ();
				Console.WriteLine (Thread.CurrentThread.Name + " Light Switch ON.");


				Console.WriteLine (Thread.CurrentThread.Name + " Waiting to Turn OFF Light Switch");
				mylightSwitch.Release ();
				Console.WriteLine (Thread.CurrentThread.Name + " Light Switch OFF.");
			}


		}

		public static void Release(){

			while (true) {
				Console.WriteLine (Thread.CurrentThread.Name + " Waiting to Turn ON Light Switch");
				mylightSwitch.Acquire ();
				Console.WriteLine (Thread.CurrentThread.Name + " Light Switch ON.");


				Console.WriteLine (Thread.CurrentThread.Name + " Waiting to Turn OFF Light Switch");
				mylightSwitch.Release ();
				Console.WriteLine (Thread.CurrentThread.Name + " Light Switch OFF.");
			}

		}

		}
	}

