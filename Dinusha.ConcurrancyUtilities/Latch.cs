using System;
using Thread = System.Threading.Thread;



namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Latch is used to block a certain amount of threads till the release method is called, then the given amount of tokens are released.
	/// so all the blocked threads could be released.
	/// </summary>
	public class Latch
	{
		private readonly Semaphore latchsem;

		/// <summary>
		/// Constructor for Latch. It initialise the semaphore 
		/// </summary>
		public Latch ()
		{
			latchsem = new Semaphore ();


		}
		/// <summary>
		/// Acquire Method keeps all threads waiting to acquire a semaphore.  
		/// </summary>
		public void Acquire(){

				latchsem.Acquire ();
				latchsem.Release ();
		}

		/// <summary>
		/// Release method, Releses all the waiting threads.
		/// </summary>
		/// <param name="a">The alpha component.</param>
		public void Release(){
		
			latchsem.Release (1);
		}
			
		
	}

}

