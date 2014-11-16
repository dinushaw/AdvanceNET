using System;
using Dinusha.ConcurrancyUtilities;
using System.Collections.Generic;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// FIFO semaphore allowes the incomming threads to aquire in the order of arrival. 
	/// This uses a First In First out order 
	/// </summary>
	public class FIFOSemaphore :Semaphore
	{
	
		Queue<Semaphore> myqueue = new Queue<Semaphore> ();
		int tokens = 0;

		public FIFOSemaphore (int ntokens) : base (ntokens)
		{
			tokens = ntokens;
		}


		/// <summary>
		/// Blocks threads until the tokens are avilable.if not threads will wait till a pulseAll command is being called. 
		/// Functionality : check whether tokens are available and decrement the value of tokens, if not wait till a token is available.
		/// </summary>
		public override void Acquire ()
		{
			lock (lockobject) {
				if (tokens > 0) {
					base.Acquire ();
					tokens--;
					return;

				}
			}

			Semaphore mypermission = new Semaphore (0);
			lock (myqueue) {
				myqueue.Enqueue (mypermission);
			
			}

			mypermission.Acquire ();

			lock (lockobject) {
				tokens--;
			}




		}
		/// <summary>
		/// Release a number of tokens. increments the token count by 1 and PulseAll and the waiting threads. Throws argument
		/// exception for negative n. 
		/// Default value for no of tokensToRelease is 1
		/// </summary>
		/// <param name="numtokens">Numtokens.</param>

		public override void Release (int numtokens=1)
		{
			Semaphore releasedSemaphore = null;
			lock (lockobject) {
				tokens++;
					
			}

			lock (myqueue) {
				if (myqueue.Count > 0) {
				
					releasedSemaphore = myqueue.Dequeue ();
					releasedSemaphore.Release (numtokens);

				} else {
					base.Release (numtokens);

				}

			}


		}
	}
}

