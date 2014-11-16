using System;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Barrier is used to block thereds untill a given number of threads are reached, 
	/// then they are being released. 
	/// Barrier can be reused.
	/// eg: if a barrier is set for 3 threds then it blocks untill 3 threads arrive at the barrier.
	/// </summary>
	public class Barrier
	{
		int count=0; 
		readonly int size;
		readonly Semaphore gotPermition;
		readonly Semaphore turnSt;

		public Barrier (int N)
		{
			size = N;
			gotPermition = new Semaphore ();
			turnSt = new Semaphore ();
		}
		/// <summary>
		/// Only method available in barrier, checks wether a certain number of threads
		/// have arrived, otherwise wait.
		/// </summary>
		public bool Arrive(){
			turnSt.Acquire ();
			lock (this) {
				count++;
				
			if (count == size) {
					gotPermition.Release (count - 1);
					count = 0;
					turnSt.Release ();
					return true;
				
			}
			}
			gotPermition.Acquire ();
			return false;

		}


	}
}

