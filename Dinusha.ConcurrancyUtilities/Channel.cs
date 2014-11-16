using System;
using Thread = System.Threading.Thread;
using System.Collections.Generic;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Channel can be used to pass data between discreet threads.
	/// </summary>
	public class Channel<T>
	{
		private readonly Semaphore takepermition;
		Queue<T> dataQueue;
		protected readonly Object lockobject = new Object();


		/// <summary>
		/// Initializes a new instance of Channel Class, Creates a Generic Queue.
		/// </summary>
		public Channel ()
		{

			takepermition = new Semaphore (0);
			dataQueue = new Queue<T> ();


		}

		/// <summary>
		/// Puts an Generic type T,  
		/// </summary>
		/// <param name="t">Generic Object </param>

		public virtual void Put (T o)
		{
			lock (lockobject) {

				dataQueue.Enqueue (o);
				takepermition.Release ();
			}

			
		}

		/// <summary>
		/// Aquires permition from the semaphore and Dequeue the Queue
		/// </summary>
		public virtual T Take ()
		{
			takepermition.Acquire ();
			lock (lockobject) {
				//	while (dataQueue.Count != 0) {
				//if (dataQueue.Count > 0) {
					return dataQueue.Dequeue ();
				//}

			}

		
		}
	}
}

