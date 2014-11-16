using System;
using System.Threading;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Reader writer lock is used to manage reading and writing to the same data store at the same time.
	/// </summary>
	public class ReaderWriterLock
	{

		Semaphore readerTS; 
		LightSwitch readersLS;



		Semaphore rlock;

		/// <summary>
		/// Initialises the readerWriterlock object. 
		/// </summary>
		public ReaderWriterLock ()
		{
			rlock = new Semaphore (1);

			readerTS = new Semaphore(1);
			readersLS = new LightSwitch(rlock);



		}


		/// <summary>
		/// Aquires permition for Reading. chechs whether there are any writers, writing to the data store.
		/// </summary>
		public void ReadersAcquire(){

			readerTS.Acquire ();
			readersLS.Acquire ();
			readerTS.Release ();


		}
		/// <summary>
		/// Signal all the waiting writers
		/// </summary>
		public void ReadersRelease(){

			readersLS.Release ();
		}

		/// <summary>
		/// Aquires a rlock semaphore and starts writing.
		/// </summary>
		public void WritersAcquire(){

			readerTS.Acquire ();
			rlock.Acquire ();

		}
		/// <summary>
		/// calls when the writer is done writing,signals all the waiting readers.
		/// </summary>
		public void WritersRelease(){

			rlock.Release ();
			readerTS.Release ();

		}

	}
}

