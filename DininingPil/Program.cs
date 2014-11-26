using System;
using Dinusha.ConcurrancyUtilities;
using Thread= System.Threading.Thread;

namespace DininingPil
{
	class MainClass
	{
		static int philcount=0;
		static Philosopher p1;
		//Semaphore forks = new Semaphore(5);
		static Semaphore[] forks = new Semaphore[]{new Semaphore(1),new Semaphore(1), new Semaphore(1), new Semaphore(1),new Semaphore(1)};
		static object lockobject = new object ();

		static Semaphore footman = new Semaphore (5);
		//static object lockobjectleft = new object ();


		public static void Main (string[] args)
		{
			/// Crating 5 philosophers 

			Philosopher p1 = new Philosopher (philcount++, forks);
			Philosopher p2 = new Philosopher (philcount++, forks);
			Philosopher p3 = new Philosopher (philcount++, forks);
			Philosopher p4 = new Philosopher (philcount++, forks);
			Philosopher p5 = new Philosopher (philcount++, forks);


		}





	}
}
