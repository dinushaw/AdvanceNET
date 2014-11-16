using System;
using Thread = System.Threading.Thread;
using System.Collections.Generic;

namespace Dinusha.ConcurrancyUtilities
{
	public class BoundChannel<T> :Channel<T>
	{
		private readonly Semaphore putPermission;

		/// <summary>
		/// Bound channel allows only a no of objects to be added to the channel. Bound of the channel is controlled by the no of tokens.
		/// used for shareing data between threads.  
		/// </summary>
		/// <param name="Size">Size.</param>
		public BoundChannel (int Size = 1)
		{
			putPermission = new Semaphore (Size);
			//dataQueue = new Queue<T> (Size);
			//QSize = Size;

		}

		/// <summary>
		/// Puts object to channel
		/// </summary>
		/// <param name="t">Generic Object</param>
		public override void Put (T t)
		{
			putPermission.Acquire ();
			base.Put (t);

		
		}
		/// <summary>
		/// Takes objects from the channel and 
		/// </summary>
		public override T Take ()
		{

			T temp = base.Take ();      
			putPermission.Release (1);
			return temp; 

		}
	}
}

