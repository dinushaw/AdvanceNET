using System;
using Thread = System.Threading.Thread;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Active objects are used insted of naked thereads, 
	/// To use , Extend this class and override the Execute method. 
	/// </summary>
	abstract class ActiveObject
	{
		Thread activeThread;

		/// <summary>
		/// Constructor which initialise the activeThread object 
		/// with 'Execute' method as a parameter 
		/// </summary>
		public ActiveObject ()
		{
			activeThread = new Thread (Execute);
		}

		/// <summary>
		/// This metod is usef to Start the thread.
		/// </summary>
		public void Start(){
			activeThread.Start ();
		}


		/// <summary>
		/// Logic for the active object. should be overridden to use. 
		/// </summary>
		protected abstract void Execute ();


		/// <summary>
		/// Stop the thread , To prevent hariming other executing threads 'Interrupt'
		/// is used insted of 'Abort'
		/// </summary>
		public void Stop(){
			activeThread.Interrupt ();
		}
	}


}

